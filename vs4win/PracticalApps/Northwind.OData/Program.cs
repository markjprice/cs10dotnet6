using Microsoft.AspNetCore.OData; // AddOData extension method
using Microsoft.OData.Edm; // IEdmModel
using Microsoft.OData.ModelBuilder; // ODataConventionModelBuilder
using Packt.Shared; // NorthwindContext and entity models

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:5004/");

// Add services to the container.

builder.Services.AddCors();

builder.Services.AddNorthwindContext();

builder.Services.AddControllers()
  .AddOData(options => options
  // register OData models including multiple versions
  .AddRouteComponents(routePrefix: "catalog",
    model: GetEdmModelForCatalog())
  .AddRouteComponents(routePrefix: "ordersystem",
    model: GetEdmModelForOrderSystem())
  .AddRouteComponents(routePrefix: "v{version}",
    model: GetEdmModelForCatalog())

  // enable query options
  .Select() // enable $select for projection
  .Expand() // enable $expand to navigate to related entities
  .Filter() // enable $filter
  .OrderBy() // enable $orderby
  .SetMaxTop(100) // enable $top
  .Count() // enable $count
);

builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new() { Title = "Northwind.OData", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(configurePolicy: options =>
{
  options.WithMethods("GET", "POST", "PUT", "DELETE");
  options.WithOrigins(
    "https://localhost:5001" // allow requests from the MVC client
  );
});

if (builder.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseSwagger();
  app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind.OData v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

IEdmModel GetEdmModelForCatalog()
{
  ODataConventionModelBuilder builder = new();
  builder.EntitySet<Category>("Categories");
  builder.EntitySet<Product>("Products");
  builder.EntitySet<Supplier>("Suppliers");
  return builder.GetEdmModel();
}

IEdmModel GetEdmModelForOrderSystem()
{
  ODataConventionModelBuilder builder = new();
  builder.EntitySet<Customer>("Customers");
  builder.EntitySet<Order>("Orders");
  builder.EntitySet<Employee>("Employees");
  builder.EntitySet<Product>("Products");
  builder.EntitySet<Shipper>("Shippers");
  return builder.GetEdmModel();
}

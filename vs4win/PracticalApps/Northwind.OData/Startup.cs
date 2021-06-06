using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Microsoft.OData.Edm; // IEdmModel
using Microsoft.OData.ModelBuilder; // ODataConventionModelBuilder
using Packt.Shared; // AddNorthwindContext extension method

namespace Northwind.OData
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    private IEdmModel GetEdmModelForCatalog()
    {
      ODataConventionModelBuilder builder = new();
      builder.EntitySet<Category>("Categories");
      builder.EntitySet<Product>("Products");
      builder.EntitySet<Supplier>("Suppliers");
      return builder.GetEdmModel();
    }

    private IEdmModel GetEdmModelForOrderSystem()
    {
      ODataConventionModelBuilder builder = new();
      builder.EntitySet<Customer>("Customers");
      builder.EntitySet<Order>("Orders");
      //builder.EntitySet<OrderDetail>("OrderDetails");
      builder.EntitySet<Product>("Products");
      builder.EntitySet<Shipper>("Shippers");
      return builder.GetEdmModel();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddNorthwindContext();

      services.AddControllers()
        .AddOData(options => options
          // register OData models including multiple versions
          .AddModel(prefix: "catalog", model: GetEdmModelForCatalog())
          .AddModel(prefix: "ordersystem", model: GetEdmModelForOrderSystem())
          .AddModel(prefix: "v{version}", model: GetEdmModelForCatalog())

          // enable query options
          .Select() // enable $select for projection
          .Expand() // enable $expand to navigate to related entities
          .Filter() // enable $filter
          .OrderBy() // enable $orderby
          .Count() // enable $count
        );

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Northwind.OData", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind.OData v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Packt.Shared; // AddNorthwindContext extension method
using Northwind.WebApi.Repositories;
using Swashbuckle.AspNetCore.SwaggerUI; // SubmitMethod
using Microsoft.AspNetCore.HttpLogging; // HttpLoggingFields

using static System.Console;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:5002/");

// Add services to the container.

builder.Services.AddCors();

builder.Services.AddNorthwindContext();

builder.Services.AddControllers(options =>
{
  WriteLine("Default output formatters:");
  foreach (IOutputFormatter formatter in options.OutputFormatters)
  {
    OutputFormatter? mediaFormatter = formatter as OutputFormatter;
    if (mediaFormatter == null)
    {
      WriteLine($"  {formatter.GetType().Name}");
    }
    else // OutputFormatter class has SupportedMediaTypes
    {
      WriteLine("  {0}, Media types: {1}",
        arg0: mediaFormatter.GetType().Name,
        arg1: string.Join(", ",
          mediaFormatter.SupportedMediaTypes));
    }
  }
})
.AddXmlDataContractSerializerFormatters()
.AddXmlSerializerFormatters();

builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new() { Title = "Northwind Service API", Version = "v1" });
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddHttpLogging(options =>
{
  options.LoggingFields = HttpLoggingFields.All;
  options.RequestBodyLogLimit = 4096; // default is 32k
  options.ResponseBodyLogLimit = 4096; // default is 32k
});

builder.Services.AddHealthChecks()
  .AddDbContextCheck<NorthwindContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(configurePolicy: options =>
{
  options.WithMethods("GET", "POST", "PUT", "DELETE");
  options.WithOrigins(
    "https://localhost:5001" // allow requests from the MVC client
  );
});

app.UseHttpLogging();

if (builder.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json",
      "Northwind Service API Version 1");

    c.SupportedSubmitMethods(new[] {
      SubmitMethod.Get, SubmitMethod.Post,
      SubmitMethod.Put, SubmitMethod.Delete });
  });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHealthChecks(path: "/howdoyoufeel");

app.UseMiddleware<SecurityHeaders>();

app.MapControllers();

app.Run();

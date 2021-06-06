using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Packt.Shared; // AddNorthwindContext extension method
using Northwind.WebApi.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

using static System.Console;

namespace Northwind.WebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddHttpLogging(options =>
      {
        //options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders;
        options.RequestBodyLogLimit = 4096; // default is 32k
        options.ResponseBodyLogLimit = 4096; // default is 32k
      });

      services.AddCors();

      services.AddNorthwindContext();

      services.AddControllers(options =>
        {
          WriteLine("Default output formatters:");
          foreach (IOutputFormatter formatter in options.OutputFormatters)
          {
            OutputFormatter mediaFormatter = formatter as OutputFormatter;
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

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo 
          { Title = "Northwind.WebApi", Version = "v1" });
      });

      services.AddScoped<ICustomerRepository, CustomerRepository>();

      services.AddHealthChecks()
        .AddDbContextCheck<NorthwindContext>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
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

      app.UseHttpLogging();

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      // must be after UseRouting and before UseEndpoints
      app.UseCors(configurePolicy: options =>
      {
        options.WithMethods("GET", "POST", "PUT", "DELETE");
        options.WithOrigins(
          "https://localhost:5001" // allow the MVC client
        );
      });

      app.UseHealthChecks(path: "/howdoyoufeel");

      app.UseMiddleware<SecurityHeaders>();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Server; // GraphQLOptions
using Packt.Shared; // AddNorthwindContext extension method

namespace Northwind.GraphQL
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
      services.AddNorthwindContext();

      services.AddControllers();

      services.AddScoped<NorthwindSchema>();

      services.AddGraphQL(
        (GraphQLOptions options, IServiceProvider provider) =>
        {
          var logger = provider.GetRequiredService<ILogger<Startup>>();

          options.UnhandledExceptionDelegate = ctx =>
              logger.LogError("{Error} occurred", ctx.OriginalException.Message);
        })
        .AddGraphTypes(typeof(NorthwindSchema), ServiceLifetime.Scoped)
        .AddDataLoader()
        .AddSystemTextJson(); // serialize responses as JSON
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseGraphQL<NorthwindSchema>(); // default path is /graphql
      app.UseGraphQLPlayground(); // default path is /ui/playground

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}

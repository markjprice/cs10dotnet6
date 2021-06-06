using Microsoft.AspNetCore.Builder; // IApplicationBuilder
using Microsoft.AspNetCore.Hosting; // IWebHostEnvironment
using Microsoft.Extensions.DependencyInjection; // IServiceCollection
using Microsoft.Extensions.Hosting; // IsDevelopment extension method
using System; // Func<T> - will not be needed with C# 10
using System.IO; // Path
using Packt.Shared; // AddNorthwindContext extension method
using System.Threading.Tasks; // Task
using Microsoft.AspNetCore.Routing; // RouteEndpoint
using Microsoft.AspNetCore.Http; // HttpContext

using static System.Console;

namespace Northwind.Web
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddRazorPages();

      services.AddNorthwindContext();
    }

    public void Configure(
      IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseRouting(); // start endpoint routing

      app.Use(async (HttpContext context, Func<Task> next) =>
      {
        RouteEndpoint rep = context.GetEndpoint() as RouteEndpoint;
        if (rep != null)
        {
          WriteLine($"Endpoint name: {rep.DisplayName}");
          WriteLine($"Endpoint route pattern: {rep.RoutePattern.RawText}");
        }

        if (context.Request.Path == "/bonjour")
        {
          // in the case of a match on URL path, this becomes a terminating
          // delegate that returns so does not call the next delegate
          await context.Response.WriteAsync("Bonjour Monde!");
          return;
        }

        // we could modify the request before calling the next delegate
        await next();
        // we could modify the response after calling the next delegate
      });

      app.UseHttpsRedirection();

      app.UseDefaultFiles(); // index.html, default.html, and so on
      app.UseStaticFiles();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapRazorPages();

        endpoints.MapGet("/hello", (Func<string>)(() => "Hello World!"));
      });
    }
  }
}
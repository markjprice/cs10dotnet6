using System.Threading.Tasks; // Task
using Microsoft.AspNetCore.Http; // RequestDelegate, HttpContext
using Microsoft.Extensions.Primitives; // StringValues

namespace Northwind.WebApi
{
  public class SecurityHeaders
  {
    private readonly RequestDelegate next;

    public SecurityHeaders(RequestDelegate next)
    {
      this.next = next;
    }

    public Task Invoke(HttpContext context)
    {
      // add any HTTP response headers you want here
      context.Response.Headers.Add(
        "super-secure", new StringValues("enable"));

      return next(context);
    }
  }
}
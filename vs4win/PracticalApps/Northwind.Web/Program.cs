using Microsoft.AspNetCore.Hosting; // UseStartup<T> extension method
using Microsoft.Extensions.Hosting; // Host
using Northwind.Web; // Startup
using System; // Console

await Host.CreateDefaultBuilder(args)
  .ConfigureWebHostDefaults(webBuilder =>
  {
    webBuilder.UseStartup<Startup>();
  }).Build().RunAsync();

Console.WriteLine("This executes after the web server has stopped!");

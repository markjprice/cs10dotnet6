using Microsoft.AspNetCore.Builder; // WebApplication
using Microsoft.AspNetCore.Hosting; // UseUrls
using Microsoft.Extensions.DependencyInjection; // AddCors
using Microsoft.Extensions.Hosting; // IsDevelopment
using System; // Func<T>
using System.Linq; // Enumerable
using Northwind.Common; // WeatherForecast

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://localhost:5003");
builder.Services.AddCors();
await using var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
}

// only allow the MVC client and only GET requests
app.UseCors(configurePolicy: options =>
{
  options.WithMethods("GET");
  options.WithOrigins(
    "https://localhost:5001"
  );
});

app.MapGet("/api/weather", (Func<WeatherForecast[]>)(() => 
{
  Random rng = new();

  return Enumerable.Range(1, 5).Select(index =>
    new WeatherForecast
    {
      Date = DateTime.Now.AddDays(index),
      TemperatureC = rng.Next(-20, 55),
      Summary = WeatherForecast.Summaries[
        rng.Next(WeatherForecast.Summaries.Length)]
    })
    .ToArray();
}));

await app.RunAsync();
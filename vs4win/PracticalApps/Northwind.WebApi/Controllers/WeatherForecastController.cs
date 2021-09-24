using Microsoft.AspNetCore.Mvc;

namespace Northwind.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
  private static readonly string[] Summaries = new[]
  {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
  };

  private readonly ILogger<WeatherForecastController> _logger;

  public WeatherForecastController(ILogger<WeatherForecastController> logger)
  {
    _logger = logger;
  }

  // GET /weatherforecast
  [HttpGet]
  public IEnumerable<WeatherForecast> Get() // original method
  {
    return Get(5); // five day forecast
  }

  // GET /weatherforecast/7
  [HttpGet("{days:int}")]
  public IEnumerable<WeatherForecast> Get(int days) // new method
  {
    return Enumerable.Range(1, days).Select(index =>
      new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      })
      .ToArray();
  }
}

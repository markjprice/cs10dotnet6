using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using System.Net.Http; // IHttpClientFactory
using System.Net.Http.Json; // ReadFromJsonAsync
using Northwind.Common; // WeatherForecast
using Grpc.Net.Client; // GrpcChannel
using System.Text;
using System.Runtime.Serialization.Formatters;

namespace Northwind.Mvc.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private NorthwindContext db;
    private readonly IHttpClientFactory clientFactory;

    public HomeController(
      ILogger<HomeController> logger,
      NorthwindContext injectedContext,
      IHttpClientFactory httpClientFactory)
    {
      _logger = logger;
      db = injectedContext;
      clientFactory = httpClientFactory;
    }

    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> Index()
    {
      _logger.LogError("This is a serious error (not really!)");
      _logger.LogWarning("This is your first warning!");
      _logger.LogWarning("Second warning!");
      _logger.LogInformation("I am in the Index method of the HomeController.");

      try
      {
        HttpClient client = clientFactory.CreateClient(
          name: "Minimal.WebApi");

        HttpRequestMessage request = new(
          method: HttpMethod.Get, requestUri: "api/weather");

        HttpResponseMessage response = await client.SendAsync(request);

        ViewData["weather"] = await response.Content
          .ReadFromJsonAsync<WeatherForecast[]>();
      }
      catch (Exception ex)
      {
        _logger.LogWarning($"Minimal.WebApi service exception: {ex.Message}");
      }

      HomeIndexViewModel model = new
      (
        VisitorCount: (new Random()).Next(1, 1001),
        Categories: await db.Categories.ToListAsync(),
        Products: await db.Products.ToListAsync()
      );
      return View(model); // pass model to view
    }

    [Route("private")]
    [Authorize(Roles = "Administrators")]
    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> ProductDetail(int? id)
    {
      if (!id.HasValue)
      {
        return BadRequest("You must pass a product ID in the route, for example, /Home/ProductDetail/21");
      }

      Product model = await db.Products
        .SingleOrDefaultAsync(p => p.ProductId == id);

      if (model == null)
      {
        return NotFound($"ProductId {id} not found.");
      }

      return View(model); // pass model to view and then return result
    }

    public IActionResult ModelBinding()
    {
      return View(); // the page with a form to submit
    }

    [HttpPost]
    public IActionResult ModelBinding(Thing thing)
    {
      HomeModelBindingViewModel model = new(thing,
        !ModelState.IsValid,
        ModelState.Values
          .SelectMany(state => state.Errors)
          .Select(error => error.ErrorMessage)
      );
      return View(model);
    }

    public IActionResult ProductsThatCostMoreThan(decimal? price)
    {
      if (!price.HasValue)
      {
        return BadRequest("You must pass a product price in the query string, for example, /Home/ProductsThatCostMoreThan?price=50");
      }

      IEnumerable<Product> model = db.Products
        .Include(p => p.Category)
        .Include(p => p.Supplier)
        .Where(p => p.UnitPrice > price);

      if (model.Count() == 0)
      {
        return NotFound(
          $"No products cost more than {price:C}.");
      }

      ViewData["MaxPrice"] = price.Value.ToString("C");
      return View(model); // pass model to view
    }

    public async Task<IActionResult> Customers(string country)
    {
      string uri;

      if (string.IsNullOrEmpty(country))
      {
        ViewData["Title"] = "All Customers Worldwide";
        uri = "api/customers/";
      }
      else
      {
        ViewData["Title"] = $"Customers in {country}";
        uri = $"api/customers/?country={country}";
      }

      HttpClient client = clientFactory.CreateClient(
        name: "Northwind.WebApi");

      HttpRequestMessage request = new(
        method: HttpMethod.Get, requestUri: uri);

      HttpResponseMessage response = await client.SendAsync(request);

      IEnumerable<Customer> model = await response.Content
        .ReadFromJsonAsync<IEnumerable<Customer>>();

      return View(model);
    }

    public async Task<IActionResult> Services()
    {
      try
      {
        HttpClient client = clientFactory.CreateClient(
          name: "Northwind.OData");

        HttpRequestMessage request = new(
          method: HttpMethod.Get, requestUri:
          "catalog/products/?$filter=startswith(ProductName, 'Cha')&$select=ProductId,ProductName,UnitPrice");

        HttpResponseMessage response = await client.SendAsync(request);

        ViewData["productsCha"] = (await response.Content
          .ReadFromJsonAsync<ODataProducts>()).Value;
      }
      catch (Exception ex)
      {
        _logger.LogWarning($"Northwind.OData service exception: {ex.Message}");
      }

      try
      {
        HttpClient client = clientFactory.CreateClient(
          name: "Northwind.GraphQL");

        HttpRequestMessage request = new(
          method: HttpMethod.Post, requestUri: "graphql");

        request.Content = new StringContent(content: @"
          query {
            products (categoryId: 8) {
              productId
              productName
              unitsInStock
            }
          }",
          encoding: Encoding.UTF8,
          mediaType: "application/graphql");

        HttpResponseMessage response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
          ViewData["seafoodProducts"] = (await response.Content
            .ReadFromJsonAsync<GraphQLProducts>()).Data.Products;
        }
      }
      catch (Exception ex)
      {
        _logger.LogWarning($"Northwind.GraphQL service exception: {ex.Message}");
      }

      try
      {
        using (GrpcChannel channel =
          GrpcChannel.ForAddress("https://localhost:5006"))
        {
          Greeter.GreeterClient greeter = new(channel);
          HelloReply reply = await greeter.SayHelloAsync(
            new HelloRequest { Name = "Henrietta" });
          ViewData["greeting"] = "Greeting from gRPC service: " + reply.Message;
        }
      }
      catch (Exception)
      {
        _logger.LogWarning($"Northwind.gRPC service is not responding.");
      }

      try
      {
        using (GrpcChannel channel =
          GrpcChannel.ForAddress("https://localhost:5006"))
        {
          Shipr.ShiprClient shipr = new(channel);

          ShipperReply reply = await shipr.GetShipperAsync(
            new ShipperRequest { ShipperId = 3 });

          ViewData["shipr"] = new Shipper
          {
            ShipperId = reply.ShipperId,
            CompanyName = reply.CompanyName,
            Phone = reply.Phone
          };
        }
      }
      catch (Exception)
      {
        _logger.LogWarning($"Northwind.gRPC service is not responding.");
      }

      return View();
    }

    public IActionResult Chat()
    {
      return View();
    }
  }
}
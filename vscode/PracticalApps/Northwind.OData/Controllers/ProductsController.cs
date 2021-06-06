using Microsoft.AspNetCore.Mvc; // IActionResult
using Microsoft.AspNetCore.OData.Query; // [EnableQuery]
using Microsoft.AspNetCore.OData.Routing.Controllers; // ODataController
using Packt.Shared; // NorthwindContext
using Microsoft.EntityFrameworkCore.Infrastructure; // GetService<T>
using Microsoft.Extensions.Logging; // ILoggerFactory

using static System.Console;

namespace Northwind.OData.Controllers
{
  public class ProductsController : ODataController
  {
    private NorthwindContext db;
    private ILoggerFactory loggerFactory;

    public ProductsController(NorthwindContext db)
    {
      this.db = db;
      loggerFactory = db.GetService<ILoggerFactory>();
      loggerFactory.AddProvider(new ConsoleLoggerProvider());
    }

    [EnableQuery]
    public IActionResult Get(string version = "1")
    {
      WriteLine($"ProductsController version {version}.");
      return Ok(db.Products);
    }

    [EnableQuery]
    public IActionResult Get(int key, string version = "1")
    {
      WriteLine($"ProductsController version {version}.");
      Product p = db.Products.Find(key);
      if (version == "2")
      {
        p.ProductName += " version 2.0";
      }
      return Ok(p);
    }

    public IActionResult Post([FromBody] Product product)
    {
      db.Products.Add(product);
      db.SaveChanges();
      return Created(product);
    }
  }
}
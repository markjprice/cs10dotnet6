using Microsoft.AspNetCore.Mvc; // IActionResult
using Microsoft.AspNetCore.OData.Query; // [EnableQuery]
using Microsoft.AspNetCore.OData.Routing.Controllers; // ODataController
using Packt.Shared; // NorthwindContext

namespace Northwind.OData.Controllers;

public class CustomersController : ODataController
{
  private readonly NorthwindContext db;

  public CustomersController(NorthwindContext db)
  {
    this.db = db;
  }

  [EnableQuery]
  public IActionResult Get()
  {
    return Ok(db.Customers);
  }

  [EnableQuery]
  public IActionResult Get(string key)
  {
    return Ok(db.Customers.Find(key));
  }
}

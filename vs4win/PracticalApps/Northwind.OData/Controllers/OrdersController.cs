using Microsoft.AspNetCore.Mvc; // IActionResult
using Microsoft.AspNetCore.OData.Query; // [EnableQuery]
using Microsoft.AspNetCore.OData.Routing.Controllers; // ODataController
using Packt.Shared; // NorthwindContext

namespace Northwind.OData.Controllers;

public class OrdersController : ODataController
{
  private readonly NorthwindContext db;

  public OrdersController(NorthwindContext db)
  {
    this.db = db;
  }

  [EnableQuery]
  public IActionResult Get()
  {
    return Ok(db.Orders);
  }

  [EnableQuery]
  public IActionResult Get(int key)
  {
    return Ok(db.Orders.Find(key));
  }
}

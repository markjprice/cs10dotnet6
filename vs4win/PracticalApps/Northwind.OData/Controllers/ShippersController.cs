using Microsoft.AspNetCore.Mvc; // IActionResult
using Microsoft.AspNetCore.OData.Query; // [EnableQuery]
using Microsoft.AspNetCore.OData.Routing.Controllers; // ODataController
using Packt.Shared; // NorthwindContext

namespace Northwind.OData.Controllers;

public class ShippersController : ODataController
{
  private readonly NorthwindContext db;

  public ShippersController(NorthwindContext db)
  {
    this.db = db;
  }

  [EnableQuery]
  public IActionResult Get()
  {
    return Ok(db.Shippers);
  }

  [EnableQuery]
  public IActionResult Get(int key)
  {
    return Ok(db.Shippers.Find(key));
  }
}

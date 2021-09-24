using Microsoft.AspNetCore.Mvc; // IActionResult
using Microsoft.AspNetCore.OData.Query; // [EnableQuery]
using Microsoft.AspNetCore.OData.Routing.Controllers; // ODataController
using Packt.Shared; // NorthwindContext

namespace Northwind.OData.Controllers;

public class SuppliersController : ODataController
{
  private readonly NorthwindContext db;

  public SuppliersController(NorthwindContext db)
  {
    this.db = db;
  }

  [EnableQuery]
  public IActionResult Get()
  {
    return Ok(db.Suppliers);
  }

  [EnableQuery]
  public IActionResult Get(int key)
  {
    return Ok(db.Suppliers.Find(key));
  }
}

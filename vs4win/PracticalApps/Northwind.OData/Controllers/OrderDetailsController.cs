using Microsoft.AspNetCore.Mvc; // IActionResult
using Microsoft.AspNetCore.OData.Query; // [EnableQuery]
using Microsoft.AspNetCore.OData.Routing.Controllers; // ODataController
using Packt.Shared; // NorthwindContext

namespace Northwind.OData.Controllers
{
  public class OrderDetailsController : ODataController
  {
    private NorthwindContext db;

    public OrderDetailsController(NorthwindContext db)
    {
      this.db = db;
    }

    [EnableQuery]
    public IActionResult Get()
    {
      return Ok(db.OrderDetails);
    }

    [EnableQuery]
    public IActionResult Get(int key)
    {
      return Ok(db.OrderDetails.Find(key));
    }
  }
}
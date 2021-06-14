using Grpc.Core; // ServerCallContext
using Microsoft.Extensions.Logging; // ILogger
using Packt.Shared; // NorthwindContext, Shipper
using System.Linq;
using System.Threading.Tasks; // Task, Task<T>

namespace Northwind.gRPC.Services
{
  public class ShipperService : Shipr.ShiprBase
  {
    private readonly ILogger<ShipperService> _logger;
    private readonly NorthwindContext db;

    public ShipperService(ILogger<ShipperService> logger,
      NorthwindContext db)
    {
      _logger = logger;
      this.db = db;
    }

    public override Task<ShipperReply> GetShipper(
      ShipperRequest request, ServerCallContext context)
    {
      return Task.FromResult(ToShipperReply(
        db.Shippers.Find(request.ShipperId)));
    }

    private ShipperReply ToShipperReply(Shipper shipper)
    {
      return new ShipperReply
      {
        ShipperId = shipper.ShipperId,
        CompanyName = shipper.CompanyName,
        Phone = shipper.Phone
      };
    }
  }
}
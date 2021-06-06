using Grpc.Core; // ServerCallContext
using Microsoft.Extensions.Logging; // ILogger
using System.Threading.Tasks; // Task, Task<T>

namespace Northwind.gRPC.Services
{
  public class GreeterService : Greeter.GreeterBase
  {
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
      _logger = logger;
    }

    public override Task<HelloReply> SayHello(
      HelloRequest request, ServerCallContext context)
    {
      return Task.FromResult(new HelloReply
      {
        Message = "Hello " + request.Name
      });
    }
  }
}
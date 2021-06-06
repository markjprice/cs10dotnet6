using GraphQL.Types; // Schema
using Microsoft.Extensions.DependencyInjection; // GetRequiredService
using Packt.Shared; // NorthwindContext
using System; // IServiceProvider

namespace Northwind.GraphQL
{
  public class NorthwindSchema : Schema
  {
    public NorthwindSchema(IServiceProvider provider) : base(provider)
    {
      // Query = new GreetQuery();
      Query = new NorthwindQuery(provider.GetRequiredService<NorthwindContext>());
    }
  }
}
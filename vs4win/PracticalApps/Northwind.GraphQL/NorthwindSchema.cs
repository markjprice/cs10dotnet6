using GraphQL.Types; // Schema
using System; // IServiceProvider
using Packt.Shared; // NorthwindContext
using Microsoft.Extensions.DependencyInjection; // GetRequiredService

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
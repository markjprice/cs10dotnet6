using GraphQL.Types; // Schema
using Packt.Shared; // NorthwindContext

namespace Northwind.GraphQL;

public class NorthwindSchema : Schema
{
  public NorthwindSchema(IServiceProvider provider) : base(provider)
  {
    // Query = new GreetQuery();
    Query = new NorthwindQuery(provider.GetRequiredService<NorthwindContext>());
  }
}

using Packt.Shared;

namespace Northwind.Mvc.Models
{
  public class GraphQLProducts
    {
      public class DataProducts
      {
        public Product[] Products { get; set; }
      }

      public DataProducts Data { get; set; }
    }
}
using Packt.Shared; // Category, Product
using System.Collections.Generic; // IList<T>

namespace Northwind.Mvc.Models
{
  public record HomeIndexViewModel
  (
    int VisitorCount,
    IList<Category> Categories,
    IList<Product> Products
  );
}

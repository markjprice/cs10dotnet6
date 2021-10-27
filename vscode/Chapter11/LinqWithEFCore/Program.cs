using Packt.Shared; // Northwind, Category, Product
using Microsoft.EntityFrameworkCore; // DbSet<T>
using System.Xml.Linq;

using static System.Console;

FilterAndSort();
// JoinCategoriesAndProducts();
// GroupJoinCategoriesAndProducts();
// AggregateProducts();
// CustomExtensionMethods();
// OutputProductsAsXml();
// ProcessSettings();

static void FilterAndSort()
{
  using (Northwind db = new())
  {
    DbSet<Product>? allProducts = db.Products;

    if (allProducts is null)
    {
      WriteLine("No products found.");
      return;
    }

    IQueryable<Product> processedProducts = allProducts
      .ProcessSequence();

    IQueryable<Product> filteredProducts = processedProducts
      .Where(product => product.UnitPrice < 10M);

    IOrderedQueryable<Product> sortedAndFilteredProducts =
      filteredProducts.OrderByDescending(product => product.UnitPrice);

    var projectedProducts = sortedAndFilteredProducts
      .Select(product => new // anonymous type
          {
        product.ProductId,
        product.ProductName,
        product.UnitPrice
      });

    WriteLine("Products that cost less than $10:");
    foreach (var p in projectedProducts)
    {
      WriteLine("{0}: {1} costs {2:$#,##0.00}",
        p.ProductId, p.ProductName, p.UnitPrice);
    }
    WriteLine();
  }
}

static void JoinCategoriesAndProducts()
{
  using (Northwind db = new())
  {
    // join every product to its category to return 77 matches 
    var queryJoin = db.Categories.Join(
      inner: db.Products,
      outerKeySelector: category => category.CategoryId,
      innerKeySelector: product => product.CategoryId,
      resultSelector: (c, p) =>
        new { c.CategoryName, p.ProductName, p.ProductId })
      .OrderBy(cp => cp.CategoryName);

    foreach (var item in queryJoin)
    {
      WriteLine("{0}: {1} is in {2}.",
        arg0: item.ProductId,
        arg1: item.ProductName,
        arg2: item.CategoryName);
    }
  }
}

static void GroupJoinCategoriesAndProducts()
{
  using (Northwind db = new())
  {
    // group all products by their category to return 8 matches 
    var queryGroup = db.Categories.AsEnumerable().GroupJoin(
      inner: db.Products,
      outerKeySelector: category => category.CategoryId,
      innerKeySelector: product => product.CategoryId,
      resultSelector: (c, matchingProducts) => new
      {
        c.CategoryName,
        Products = matchingProducts.OrderBy(p => p.ProductName)
      });

    foreach (var category in queryGroup)
    {
      WriteLine("{0} has {1} products.",
        arg0: category.CategoryName,
        arg1: category.Products.Count());

      foreach (var product in category.Products)
      {
        WriteLine($"  {product.ProductName}");
      }
    }
  }
}

static void AggregateProducts()
{
  using (Northwind db = new())
  {
    WriteLine("{0,-25} {1,10}",
      arg0: "Product count:",
      arg1: db.Products.Count());

    WriteLine("{0,-25} {1,10:$#,##0.00}",
      arg0: "Highest product price:",
      arg1: db.Products.Max(p => p.UnitPrice));

    WriteLine("{0,-25} {1,10:N0}",
      arg0: "Sum of units in stock:",
      arg1: db.Products.Sum(p => p.UnitsInStock));

    WriteLine("{0,-25} {1,10:N0}",
      arg0: "Sum of units on order:",
      arg1: db.Products.Sum(p => p.UnitsOnOrder));

    WriteLine("{0,-25} {1,10:$#,##0.00}",
      arg0: "Average unit price:",
      arg1: db.Products.Average(p => p.UnitPrice));

    WriteLine("{0,-25} {1,10:$#,##0.00}",
      arg0: "Value of units in stock:",
      arg1: db.Products
        .Sum(p => p.UnitPrice * p.UnitsInStock));
  }
}

static void CustomExtensionMethods()
{
  using (Northwind db = new())
  {
    WriteLine("Mean units in stock: {0:N0}",
      db.Products.Average(p => p.UnitsInStock));

    WriteLine("Mean unit price: {0:$#,##0.00}",
      db.Products.Average(p => p.UnitPrice));

    WriteLine("Median units in stock: {0:N0}",
      db.Products.Median(p => p.UnitsInStock));

    WriteLine("Median unit price: {0:$#,##0.00}",
      db.Products.Median(p => p.UnitPrice));

    WriteLine("Mode units in stock: {0:N0}",
      db.Products.Mode(p => p.UnitsInStock));

    WriteLine("Mode unit price: {0:$#,##0.00}",
      db.Products.Mode(p => p.UnitPrice));
  }
}

static void OutputProductsAsXml()
{
  using (Northwind db = new())
  {
    Product[] productsArray = db.Products.ToArray();

    XElement xml = new("products",
      from p in productsArray
      select new XElement("product",
        new XAttribute("id", p.ProductId),
        new XAttribute("price", p.UnitPrice),
        new XElement("name", p.ProductName)));

    WriteLine(xml.ToString());
  }
}

static void ProcessSettings()
{
  XDocument doc = XDocument.Load("settings.xml");

  var appSettings = doc.Descendants("appSettings")
    .Descendants("add")
    .Select(node => new
    {
      Key = node.Attribute("key")?.Value,
      Value = node.Attribute("value")?.Value
    }).ToArray();

  foreach (var item in appSettings)
  {
    WriteLine($"{item.Key}: {item.Value}");
  }
}


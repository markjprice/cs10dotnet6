using Microsoft.EntityFrameworkCore; // Include extension method
using Microsoft.EntityFrameworkCore.Infrastructure; // GetService extension method
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.ChangeTracking; // CollectionEntry
using Microsoft.EntityFrameworkCore.Storage; // IDbContextTransaction
using Packt.Shared;

using static System.Console;

WriteLine($"Using {ProjectConstants.DatabaseProvider} database provider.");

// QueryingCategories();
// FilteredIncludes();
QueryingProducts();
// QueryingWithLike();

/*
if (AddProduct(categoryId: 6,
  productName: "Bob's Burgers", price: 500M))
{
  WriteLine("Add product successful.");
}
*/

/*
if (IncreaseProductPrice(
  productNameStartsWith: "Bob", amount: 20M))
{
  WriteLine("Update product price successful.");
}
*/

/*
int deleted = DeleteProducts(productNameStartsWith: "Bob");
WriteLine($"{deleted} product(s) were deleted.");
*/

// ListProducts();


static void QueryingCategories()
{
  using (Northwind db = new())
  {
    ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
    loggerFactory.AddProvider(new ConsoleLoggerProvider());

    WriteLine("Categories and how many products they have:");

    // a query to get all categories and their related products 
    IQueryable<Category>? categories;
      // = db.Categories;
      //.Include(c => c.Products);
    
    db.ChangeTracker.LazyLoadingEnabled = false;

    Write("Enable eager loading? (Y/N): ");
    bool eagerloading = (ReadKey().Key == ConsoleKey.Y);
    bool explicitloading = false;
    WriteLine();

    if (eagerloading)
    {
      categories = db.Categories?.Include(c => c.Products);
    }
    else
    {
      categories = db.Categories;

      Write("Enable explicit loading? (Y/N): ");
      explicitloading = (ReadKey().Key == ConsoleKey.Y);
      WriteLine();
    }

    if ((categories is null) || (!categories.Any()))
    {
      WriteLine("No categories found.");
      return;
    }

    foreach (Category c in categories)
    {
      if (explicitloading)
      {
        Write($"Explicitly load products for {c.CategoryName}? (Y/N): ");
        ConsoleKeyInfo key = ReadKey();
        WriteLine();
        if (key.Key == ConsoleKey.Y)
        {
          CollectionEntry<Category, Product> products =
            db.Entry(c).Collection(c2 => c2.Products);
          if (!products.IsLoaded) products.Load();
        }
      }
      WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
    }
  }
}

static void FilteredIncludes()
{
  using (Northwind db = new())
  {
    Write("Enter a minimum for units in stock: ");
    string unitsInStock = ReadLine() ?? "10";
    int stock = int.Parse(unitsInStock);

    IQueryable<Category>? categories = db.Categories?
      .Include(c => c.Products.Where(p => p.Stock >= stock));

    if ((categories is null) || (!categories.Any()))
    {
      WriteLine("No categories found.");
      return;
    }

    WriteLine($"ToQueryString: {categories.ToQueryString()}");

    foreach (Category c in categories)
    {
      WriteLine($"{c.CategoryName} has {c.Products.Count} products with a minimum of {stock} units in stock.");

      foreach (Product p in c.Products)
      {
        WriteLine($"  {p.ProductName} has {p.Stock} units in stock.");
      }
    }
  }
}

static void QueryingProducts()
{
  using (Northwind db = new())
  {
    ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
    loggerFactory.AddProvider(new ConsoleLoggerProvider());

    WriteLine("Products that cost more than a price, highest at top.");
    string? input;
    decimal price;

    do
    {
      Write("Enter a product price: ");
      input = ReadLine();
    } while (!decimal.TryParse(input, out price));

    IQueryable<Product>? products = db.Products?
      .Where(product => product.Cost > price)
      .OrderByDescending(product => product.Cost);

    if ((products is null) || (!products.Any()))
    {
      WriteLine("No products found.");
      return;
    }

    foreach (Product p in products)
    {
      WriteLine(
        "{0}: {1} costs {2:$#,##0.00} and has {3} in stock.",
        p.ProductId, p.ProductName, p.Cost, p.Stock);
    }
  }
}

static void QueryingWithLike()
{
  using (Northwind db = new())
  {
    ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
    loggerFactory.AddProvider(new ConsoleLoggerProvider());

    Write("Enter part of a product name: ");
    string? input = ReadLine();

    IQueryable<Product>? products = db.Products?
      .Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));

    if ((products is null) || (!products.Any()))
    {
      WriteLine("No products found.");
      return;
    }

    foreach (Product p in products)
    {
      WriteLine("{0} has {1} units in stock. Discontinued? {2}",
        p.ProductName, p.Stock, p.Discontinued);
    }
  }
}

static bool AddProduct(int categoryId, string productName, decimal? price)
{
  using (Northwind db = new())
  {
    Product p = new()
    {
      CategoryId = categoryId,
      ProductName = productName,
      Cost = price
    };

    // mark product as added in change tracking
    db.Products.Add(p);

    // save tracked changes to database 
    int affected = db.SaveChanges();
    return (affected == 1);
  }
}

static void ListProducts()
{
  using (Northwind db = new())
  {
    WriteLine("{0,-3} {1,-35} {2,8} {3,5} {4}",
      "ID", "Product Name", "Cost", "Stock", "Disc.");

    foreach (Product p in db.Products
      .OrderByDescending(product => product.Cost))
    {
      WriteLine("{0:000} {1,-35} {2,8:$#,##0.00} {3,5} {4}",
        p.ProductId, p.ProductName, p.Cost, p.Stock, p.Discontinued);
    }
  }
}

static bool IncreaseProductPrice(
  string productNameStartsWith, decimal amount)
{
  using (Northwind db = new())
  {
    // get first product whose name starts with name
    Product updateProduct = db.Products.First(
      p => p.ProductName.StartsWith(productNameStartsWith));

    updateProduct.Cost += amount;

    int affected = db.SaveChanges();
    return (affected == 1);
  }
}

static int DeleteProducts(string productNameStartsWith)
{
  using (Northwind db = new())
  {
    using (IDbContextTransaction t = db.Database.BeginTransaction())
    {
      WriteLine("Transaction isolation level: {0}",
        t.GetDbTransaction().IsolationLevel);

      IQueryable<Product>? products = db.Products?.Where(
        p => p.ProductName.StartsWith(productNameStartsWith));

      if (products is null)
      {
        WriteLine("No products found to delete.");
        return 0;
      }
      else
      {
        db.Products.RemoveRange(products);
      }

      int affected = db.SaveChanges();
      t.Commit();
      return affected;
    }
  }
}

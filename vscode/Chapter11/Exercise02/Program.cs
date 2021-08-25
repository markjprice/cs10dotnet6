using System.Xml; // XmlWriter
using System.Text.Json; // 
using Microsoft.EntityFrameworkCore; // Include extension method
using Packt.Shared; // Northwind, Category, Product

using static System.Console;
using static System.IO.Path;
using static System.Environment;

WriteLine("Creating four files containing serialized categories and products...");

using (Northwind db = new())
{
  // a query to get all categories and their related products 
  IQueryable<Category>? categories = db.Categories?.Include(c => c.Products);

  if (categories is null)
  {
    WriteLine("No categories found.");
    return;
  }

  GenerateXmlFile(categories);
  GenerateXmlFile(categories, useAttributes: false);
  GenerateCsvFile(categories);
  GenerateJsonFile(categories);
}

// we want to easily show the difference between outputting
// XML using elements or attributes so we will define a 
// delegate to reference the two different methods.

static void GenerateXmlFile(
  IQueryable<Category> categories, bool useAttributes = true)
{
  string which = useAttributes ? "attibutes" : "elements";

  string xmlFile = $"categories-and-products-using-{which}.xml";

  using (FileStream xmlStream = File.Create(
    Combine(CurrentDirectory, xmlFile)))
  {
    using (XmlWriter xml = XmlWriter.Create(xmlStream,
      new XmlWriterSettings { Indent = true }))
    {

      WriteDataDelegate writeMethod;

      if (useAttributes)
      {
        writeMethod = xml.WriteAttributeString;
      }
      else // use elements
      {
        writeMethod = xml.WriteElementString;
      }

      xml.WriteStartDocument();
      xml.WriteStartElement("categories");

      foreach (Category c in categories)
      {
        xml.WriteStartElement("category");
        writeMethod("id", c.CategoryId.ToString());
        writeMethod("name", c.CategoryName);
        writeMethod("desc", c.Description);
        writeMethod("product_count", c.Products.Count.ToString());
        xml.WriteStartElement("products");

        foreach (Product p in c.Products)
        {
          xml.WriteStartElement("product");

          writeMethod("id", p.ProductId.ToString());
          writeMethod("name", p.ProductName);
          writeMethod("cost", p.Cost is null ? "0" : p.Cost.Value.ToString());
          writeMethod("stock", p.Stock.ToString());
          writeMethod("discontinued", p.Discontinued.ToString());

          xml.WriteEndElement(); // </product>
        }
        xml.WriteEndElement(); // </products>
        xml.WriteEndElement(); // </category>
      }
      xml.WriteEndElement(); // </categories>
    }
  }

  WriteLine("{0} contains {1:N0} bytes.",
    arg0: xmlFile,
    arg1: new FileInfo(xmlFile).Length);
}

static void GenerateCsvFile(IQueryable<Category> categories)
{
  string csvFile = "categories-and-products.csv";

  using (FileStream csvStream = File.Create(Combine(CurrentDirectory, csvFile)))
  {
    using (StreamWriter csv = new(csvStream))
    {

      csv.WriteLine("CategoryId,CategoryName,Description,ProductId,ProductName,Cost,Stock,Discontinued");

      foreach (Category c in categories)
      {
        foreach (Product p in c.Products)
        {
          csv.Write("{0},\"{1}\",\"{2}\",",
            arg0: c.CategoryId,
            arg1: c.CategoryName,
            arg2: c.Description);

          csv.Write("{0},\"{1}\",{2},",
            arg0: p.ProductId,
            arg1: p.ProductName,
            arg2: p.Cost is null ? 0 :p.Cost.Value);

          csv.WriteLine("{0},{1}",
            arg0: p.Stock,
            arg1: p.Discontinued);
        }
      }
    }
  }

  WriteLine("{0} contains {1:N0} bytes.",
    arg0: csvFile,
    arg1: new FileInfo(csvFile).Length);
}

static void GenerateJsonFile(IQueryable<Category> categories)
{
  string jsonFile = "categories-and-products.json";

  using (FileStream jsonStream = File.Create(Combine(CurrentDirectory, jsonFile)))
  {
    using (Utf8JsonWriter json = new(jsonStream,
      new JsonWriterOptions { Indented = true }))
    {
      json.WriteStartObject();
      json.WriteStartArray("categories");

      foreach (Category c in categories)
      {
        json.WriteStartObject();

        json.WriteNumber("id", c.CategoryId);
        json.WriteString("name", c.CategoryName);
        json.WriteString("desc", c.Description);
        json.WriteNumber("product_count", c.Products.Count);

        json.WriteStartArray("products");

        foreach (Product p in c.Products)
        {
          json.WriteStartObject();

          json.WriteNumber("id", p.ProductId);
          json.WriteString("name", p.ProductName);
          json.WriteNumber("cost", p.Cost is null ? 0 : p.Cost.Value);
          json.WriteNumber("stock", p.Stock is null ? 0 : p.Stock.Value);
          json.WriteBoolean("discontinued", p.Discontinued);

          json.WriteEndObject(); // product
        }
        json.WriteEndArray(); // products
        json.WriteEndObject(); // category
      }
      json.WriteEndArray(); // categories
      json.WriteEndObject();
    }
  }

  WriteLine("{0} contains {1:N0} bytes.",
    arg0: jsonFile,
    arg1: new FileInfo(jsonFile).Length);
}

delegate void WriteDataDelegate(string name, string? value);

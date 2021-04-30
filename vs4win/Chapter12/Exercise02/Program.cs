using System.Linq;
using static System.Console;
using Packt.Shared;

namespace Exercise02
{
  class Program
  {
    static void Main(string[] args)
    {
      using (var db = new Northwind())
      {
        IQueryable<string> distinctCities = 
          db.Customers.Select(c => c.City).Distinct();

        WriteLine("A list of cities that at least one customers resides in:");
        WriteLine($"{string.Join(", ", distinctCities)}");
        WriteLine();

        Write("Enter the name of a city: ");
        var city = ReadLine();

        var customersInCity = db.Customers.Where(c => c.City == city);

        WriteLine($"There are {customersInCity.Count()} customers in {city}:");
        foreach (var item in customersInCity)
        {
          WriteLine($"{item.CompanyName}");
        }
      }
    }
  }
}
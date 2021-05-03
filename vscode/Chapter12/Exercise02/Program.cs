using System.Linq;
using static System.Console;
using Packt.Shared;

namespace Exercise02
{
  class Program
  {
    static void Main(string[] args)
    {
      using (Northwind db = new())
      {
        IQueryable<string> distinctCities = 
          db.Customers.Select(c => c.City).Distinct();

        WriteLine("A list of cities that at least one customers resides in:");
        WriteLine($"{string.Join(", ", distinctCities)}");
        WriteLine();

        Write("Enter the name of a city: ");
        string city = ReadLine();

        IQueryable<Customer> customersInCity = db.Customers.Where(c => c.City == city);

        WriteLine($"There are {customersInCity.Count()} customers in {city}:");
        foreach (Customer c in customersInCity)
        {
          WriteLine($"{c.CompanyName}");
        }
      }
    }
  }
}
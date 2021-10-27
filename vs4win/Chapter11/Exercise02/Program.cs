using Packt.Shared;

using static System.Console;

using (Northwind db = new())
{
  IQueryable<string?>? distinctCities =
    db.Customers?.Select(c => c.City).Distinct();

  if (distinctCities is null)
  {
    WriteLine("No distinct cities found.");
    return;
  }

  WriteLine("A list of cities that at least one customer resides in:");
  WriteLine($"{string.Join(", ", distinctCities)}");
  WriteLine();

  Write("Enter the name of a city: ");
  string? city = ReadLine();

  IQueryable<Customer>? customersInCity = db.Customers?.Where(c => c.City == city);

  if (customersInCity is null)
  {
    WriteLine($"No customers found in {city}.");
    return;
  }

  WriteLine($"There are {customersInCity.Count()} customers in {city}:");
  foreach (Customer c in customersInCity)
  {
    WriteLine($"  {c.CompanyName}");
  }
}

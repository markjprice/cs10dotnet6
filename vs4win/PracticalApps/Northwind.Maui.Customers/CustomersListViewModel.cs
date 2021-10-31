using System.Collections.ObjectModel; // ObservableCollection<T>

namespace Northwind.Maui.Customers;

public class CustomersListViewModel : ObservableCollection<CustomerDetailViewModel>
{
  // for testing before calling web service
  public void AddSampleData(bool clearList = true)
  {
    if (clearList) Clear();

    Add(new CustomerDetailViewModel
    {
      CustomerId = "ALFKI",
      CompanyName = "Alfreds Futterkiste",
      ContactName = "Maria Anders",
      City = "Berlin",
      Country = "Germany",
      Phone = "030-0074321"
    });

    Add(new CustomerDetailViewModel
    {
      CustomerId = "FRANK",
      CompanyName = "Frankenversand",
      ContactName = "Peter Franken",
      City = "München",
      Country = "Germany",
      Phone = "089-0877310"
    });

    Add(new CustomerDetailViewModel
    {
      CustomerId = "SEVES",
      CompanyName = "Seven Seas Imports",
      ContactName = "Hari Kumar",
      City = "London",
      Country = "UK",
      Phone = "(171) 555-1717"
    });
  }
}

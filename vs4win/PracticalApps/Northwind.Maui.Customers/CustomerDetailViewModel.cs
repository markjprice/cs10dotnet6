using System.ComponentModel; // INotifyPropertyChanged
using System.Runtime.CompilerServices; // [CallerMemberName]

namespace Northwind.Maui.Customers;

public class CustomerDetailViewModel : INotifyPropertyChanged
{
  public event PropertyChangedEventHandler PropertyChanged;

  private string customerId;
  private string companyName;
  private string contactName;
  private string city;
  private string country;
  private string phone;

  // this attribute sets the propertyName parameter
  // using the context in which this method is called
  private void NotifyPropertyChanged(
    [CallerMemberName] string propertyName = "")
  {
    // if an event handler has been set then invoke
    // the delegate and pass the name of the property
    PropertyChanged?.Invoke(this,
      new PropertyChangedEventArgs(propertyName));
  }

  public string CustomerId
  {
    get => customerId;
    set
    {
      customerId = value;
      NotifyPropertyChanged();
    }
  }

  public string CompanyName
  {
    get => companyName;
    set
    {
      companyName = value;
      NotifyPropertyChanged();
    }
  }

  public string ContactName
  {
    get => contactName;
    set
    {
      contactName = value;
      NotifyPropertyChanged();
    }
  }

  public string City
  {
    get => city;
    set
    {
      city = value;
      NotifyPropertyChanged();
      NotifyPropertyChanged(nameof(Location));
    }
  }

  public string Country
  {
    get => country;
    set
    {
      country = value;
      NotifyPropertyChanged();
      NotifyPropertyChanged(nameof(Location));
    }
  }

  public string Phone
  {
    get => phone;
    set
    {
      phone = value;
      NotifyPropertyChanged();
    }
  }

  public string Location
  {
    get => $"{City}, {Country}";
  }
}
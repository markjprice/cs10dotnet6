using Microsoft.Maui.Controls;
using System;
using Northwind.Maui.Customers.ViewModels;

namespace Northwind.Maui.Customers.Views
{
  public partial class CustomerDetailPage : ContentPage
  {
    private CustomersViewModel customers;

    public CustomerDetailPage(CustomersViewModel customers)
    {
      InitializeComponent();
      this.customers = customers;
      BindingContext = new CustomerViewModel();
      Title = "Add Customer";
    }

    public CustomerDetailPage(CustomersViewModel customers, CustomerViewModel customer)
    {
      InitializeComponent();
      this.customers = customers;
      BindingContext = customer;
      InsertButton.IsVisible = false;
    }

    async void InsertButton_Clicked(object sender, EventArgs e)
    {
      customers.Add((CustomerViewModel)BindingContext);
      await Navigation.PopAsync(animated: true);
    }
  }
}
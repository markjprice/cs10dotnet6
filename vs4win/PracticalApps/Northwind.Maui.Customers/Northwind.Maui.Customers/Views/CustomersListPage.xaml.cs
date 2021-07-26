using Microsoft.Maui.Controls;
using System;
using Northwind.Maui.Customers.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Northwind.Maui.Customers.Views
{
  public partial class CustomersListPage : ContentPage
  {
    public CustomersListPage()
    {
      InitializeComponent();

      CustomersViewModel viewModel = new();

      try
      {
        HttpClient client = new()
        {
          BaseAddress = new Uri("http://localhost:5008/")
        };

        client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = client
          .GetAsync("api/customers").Result;

        response.EnsureSuccessStatusCode();

        string content = response.Content
          .ReadAsStringAsync().Result;

        IEnumerable<CustomerViewModel> customersFromService = JsonConvert
          .DeserializeObject<IEnumerable<CustomerViewModel>>(content);

        foreach (CustomerViewModel c in customersFromService
          .OrderBy(customer => customer.CompanyName))
        {
          viewModel.Add(c);
        }
      }
      catch (Exception ex)
      {
        ErrorBox.IsVisible = true;
        ErrorBox.Text = $"App will use sample data due to: {ex.Message}";

        viewModel.AddSampleData();
      }

      BindingContext = viewModel;
    }

    async void Customer_Tapped(object sender, ItemTappedEventArgs e)
    {
      CustomerViewModel c = e.Item as CustomerViewModel;
      if (c == null) return;

      // navigate to the detail view and show the tapped customer
      await Navigation.PushAsync(new CustomerDetailPage(
        BindingContext as CustomersViewModel, c));
    }

    async void Customers_Refreshing(object sender, EventArgs e)
    {
      ListView listView = sender as ListView;
      if (listView == null) return;

      listView.IsRefreshing = true;

      // simulate a refresh
      await Task.Delay(1500);

      listView.IsRefreshing = false;
    }

    void Customer_Deleted(object sender, EventArgs e)
    {
      MenuItem menuItem = sender as MenuItem;
      CustomerViewModel c = menuItem.BindingContext as CustomerViewModel;
      if (c == null) return;
      (BindingContext as CustomersViewModel).Remove(c);
    }

    async void Customer_Phoned(object sender, EventArgs e)
    {
      MenuItem menuItem = sender as MenuItem;
      CustomerViewModel c = menuItem.BindingContext as CustomerViewModel;

      if (await this.DisplayAlert("Dial a Number",
        "Would you like to call " + c.Phone + "?",
        "Yes", "No"))
      {
        //IDialer dialer = DependencyService.Get<IDialer>();

        //if (dialer != null) dialer.Dial(c.Phone);
      }
    }

    async void Add_Clicked(object sender, EventArgs e)
    {
      await Navigation.PushAsync(new CustomerDetailPage(
        BindingContext as CustomersViewModel));
    }
  }
}
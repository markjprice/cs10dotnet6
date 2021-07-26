using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using Northwind.Maui.Customers.Views;
using Application = Microsoft.Maui.Controls.Application;

namespace Northwind.Maui.Customers
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();
    }

    protected override IWindow CreateWindow(IActivationState activationState)
    {
      this.On<Microsoft.Maui.Controls.PlatformConfiguration.Windows>()
        .SetImageDirectory("Assets");

      return new Window(new NavigationPage(new CustomersListPage()));
    }
  }
}

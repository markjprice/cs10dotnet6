using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace Northwind.Maui.Customers
{
  public class Startup : IStartup
  {
    public void Configure(IAppHostBuilder appBuilder)
    {
      appBuilder
        .UseMauiApp<App>()
        .ConfigureFonts(fonts =>
        {
          fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        });
    }
  }
}
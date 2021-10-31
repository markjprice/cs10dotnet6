using Android.App;
using Android.Runtime;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System;

namespace Northwind.Maui.Customers
{
  [Application(UsesCleartextTraffic = true)]
  public class MainApplication : MauiApplication
  {
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
      : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
  }
}
using System;
using static System.Console;

namespace DotNetCoreEverywhere
{
  class Program
  {
    static void Main(string[] args)
    {
      WriteLine("I can run everywhere!");

      WriteLine($"OS Version is {Environment.OSVersion}.");

      if (OperatingSystem.IsMacOS())
      {
        WriteLine("I am macOS.");
      }
      else if (OperatingSystem.IsWindowsVersionAtLeast(major: 10))
      {
        WriteLine("I am Windows 10.");
      }

      WriteLine("Press ENTER to stop me.");
      ReadLine();
    }
  }
}
using static System.Console;

namespace Packt;

public class Calculator
{
  public static void Gamma() // public so it can be called from outside
  {
    WriteLine("In Gamma");
    Delta();
  }

  private static void Delta() // private so it can only be called internally
  {
    WriteLine("In Delta");
    File.OpenText("bad file path");
  }
}

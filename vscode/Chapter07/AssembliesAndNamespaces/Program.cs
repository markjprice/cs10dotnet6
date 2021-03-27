using System;
using System.Xml.Linq;
using static System.Console;
using Packt.Shared;
using DialectSoftware.Collections;
using DialectSoftware.Collections.Generics;

namespace AssembliesAndNamespaces
{
  class Program
  {
    static void Main(string[] args)
    {
      XDocument doc = new();

      string s1 = "Hello";
      String s2 = "World";

      WriteLine($"{s1} {s2}");

      // Testing your class library package

      Write("Enter a color value in hex: ");
      string hex = "00ffc8"; // or ReadLine();
      WriteLine("Is {0} a valid color value? {1}",
        arg0: hex, arg1: hex.IsValidHex());

      Write("Enter a XML element: ");
      string xmlTag = "<h1 class=\"<\" />"; // or ReadLine();
      WriteLine("Is {0} a valid XML element? {1}",
        arg0: xmlTag, arg1: xmlTag.IsValidXmlTag());

      Write("Enter a password: ");
      string password = "secretsauce"; // or ReadLine();
      WriteLine("Is {0} a valid password? {1}",
        arg0: password, arg1: password.IsValidPassword());

      // Using non-.NET Standard libraries

      var x = new Axis("x", 0, 10, 1);
      var y = new Axis("y", 0, 4, 1);

      var matrix = new Matrix<long>(new[] { x, y });

      for (int i = 0; i < matrix.Axes[0].Points.Length; i++)
      {
        matrix.Axes[0].Points[i].Label = "x" + i.ToString();
      }

      for (int i = 0; i < matrix.Axes[1].Points.Length; i++)
      {
        matrix.Axes[1].Points[i].Label = "y" + i.ToString();
      }

      foreach (long[] c in matrix)
      {
        matrix[c] = c[0] + c[1];
      }

      foreach (long[] c in matrix)
      {
        WriteLine("{0},{1} ({2},{3}) = {4}",
          matrix.Axes[0].Points[c[0]].Label,
          matrix.Axes[1].Points[c[1]].Label,
          c[0], c[1], matrix[c]);
      }

    }
  }
}

using System;
using static System.Console;

namespace Exercise02
{
  class Program
  {
    static void Main(string[] args)
    {
      var r = new Rectangle(3, 4.5);
      WriteLine($"Rectangle H: {r.Height}, W: {r.Width}, Area: {r.Area}");

      var s = new Square(5);
      WriteLine($"Square    H: {s.Height}, W: {s.Width}, Area: {s.Area}");

      var c = new Circle(2.5);
      WriteLine($"Circle    H: {c.Height}, W: {c.Width}, Area: {c.Area}");
    }
  }
}
using System;
using static System.Console;

namespace Exercise04
{
  class Program
  {
    static void Main(string[] args)
    {
      Write("Enter a number between 0 and 255: ");
      string n1 = ReadLine();

      Write("Enter another number between 0 and 255: ");
      string n2 = ReadLine();

      try
      {
        byte a = byte.Parse(n1);
        byte b = byte.Parse(n2);

        var answer = a / b;

        WriteLine($"{a} divided by {b} is {answer}");
      }
      catch (Exception ex)
      {
        WriteLine($"{ex.GetType().Name}: {ex.Message}");
      }
    }
  }
}
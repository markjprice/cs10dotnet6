using static System.Console;
using Packt.Shared;
using System.Numerics;

namespace Exercise03App
{
  class Program
  {
    static void Main(string[] args)
    {
      Write("Enter a number up to twenty one digits long: ");
      string input = ReadLine();

      if (input.Length > 21)
      {
        WriteLine("I cannot handle more than twenty one digits!");
        return;
      }

      var number = BigInteger.Parse(input);

      WriteLine($"{number:N0} in words is {number.ToWords()}.");
    }
  }
}
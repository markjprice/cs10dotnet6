using System;
using System.Text.RegularExpressions;
using static System.Console;

namespace Exercise02
{
  class Program
  {
    static void Main(string[] args)
    {
      WriteLine("The default regular expression checks for at least one digit.");

      do
      {
        Write("Enter a regular expression (or press ENTER to use the default): ");
        string regexp = ReadLine();

        if (string.IsNullOrWhiteSpace(regexp))
        {
          regexp = @"^\d+$";
        }

        Write("Enter some input: ");
        string input = ReadLine();

        var r = new Regex(regexp);

        WriteLine($"{input} matches {regexp}: {r.IsMatch(input)}");

        WriteLine("Press ESC to end or any key to try again.");
      }
      while (ReadKey().Key != ConsoleKey.Escape);
    }
  }
}
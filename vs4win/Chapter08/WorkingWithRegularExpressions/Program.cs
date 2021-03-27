using System;
using static System.Console;
using System.Text.RegularExpressions;

namespace WorkingWithRegularExpressions
{
  class Program
  {
    static void Main(string[] args)
    {
      Write("Enter your age: ");
      string input = ReadLine();

      var ageChecker = new Regex(@"^\d+$");

      if (ageChecker.IsMatch(input))
      {
        WriteLine("Thank you!");
      }
      else
      {
        WriteLine($"This is not a valid age: {input}");
      }

      string films = "\"Monsters, Inc.\",\"I, Tonya\",\"Lock, Stock and Two Smoking Barrels\"";

      WriteLine($"Films to split: {films}");

      string[] filmsDumb = films.Split(',');

      WriteLine("Splitting with string.Split method:");
      foreach (string film in filmsDumb)
      {
        WriteLine(film);
      }

      WriteLine();

      var csv = new Regex(
        "(?:^|,)(?=[^\"]|(\")?)\"?((?(1)[^\"]*|[^,\"]*))\"?(?=,|$)");

      MatchCollection filmsSmart = csv.Matches(films);

      WriteLine("Splitting with regular expression:");
      foreach (Match film in filmsSmart)
      {
        WriteLine(film.Groups[2].Value);
      }
    }
  }
}

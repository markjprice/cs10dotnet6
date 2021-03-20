using static System.Console;

namespace PrimeFactors
{
  class Program
  {
    static void Main(string[] args)
    {
      Write("Enter a number between 1 and 1000: ");

      if (int.TryParse(ReadLine(), out int number))
      {
        WriteLine(format: "Prime factors of {0} are: {1}",
          arg0: number,
          arg1: Primes.PrimeFactors(number));
      }
    }
  }
}
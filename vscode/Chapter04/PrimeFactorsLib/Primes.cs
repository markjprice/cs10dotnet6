namespace PrimeFactors
{
  public class Primes
  {
    public static int[] PrimeNumbers = new[]
    {
      97, 89, 83, 79, 73, 71, 67, 61, 59, 53,
      47, 43, 41, 37, 31, 29, 23, 19, 17, 13,
      11, 7, 5, 3, 2
    };

    public static string PrimeFactors(int number)
    {
      string factors = string.Empty;

      foreach (int divisor in PrimeNumbers)
      {
        int remainder;
        do
        {
          remainder = number % divisor;
          if (remainder == 0)
          {
            number = number / divisor;
            if (number == 1)
            {
              factors += $"{divisor}";
            }
            else
            {
              factors += $"{divisor} x ";
            }
          }
        } while (remainder == 0);
      }
      return $"{factors}";
    }
  }
}
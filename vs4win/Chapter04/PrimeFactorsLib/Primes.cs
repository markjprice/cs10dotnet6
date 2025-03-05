﻿namespace PrimeFactorsLib;

public static class Primes
{
  public static readonly int[] PrimeNumbers = new[]
  {
  /*
      997, 991, 983, 977, 971, 967, 953,
      947, 941, 937, 929, 919, 911, 907, 887,
      883, 881, 877, 863, 859, 857, 853, 839,
      829, 827, 823, 821, 811, 809, 797, 787,
      773, 769, 761, 757, 751, 743, 739, 733,
      727, 719, 709, 701, 691, 683, 677, 673,
      661, 659, 653, 647, 643, 641, 631, 619,
      617, 613, 607, 601, 599, 593, 587, 577,
      571, 569, 563, 557, 547, 541, 523, 521,
      509, 503, 
  */
      499, 491, 487, 479, 467, 463,
      461, 457, 449, 443, 439, 433, 431, 421,
      419, 409, 401, 397, 389, 383, 379, 373,
      367, 359, 353, 349, 347, 337, 331, 317,
      313, 311, 307, 293, 283, 281, 277, 271,
      269, 263, 257, 251, 241, 239, 233, 229,
      227, 223, 211, 199, 197, 193, 191, 181,
      179, 173, 167, 163, 157, 151, 149, 139,
      137, 131, 127, 113, 109, 107, 103, 101,
      97, 89, 83, 79, 73, 71, 67, 61, 59, 53,
      47, 43, 41, 37, 31, 29, 23, 19, 17, 13,
      11, 7, 5, 3, 2
  };

  public static string PrimeFactors(int number)
  {
    var factors = string.Empty;

    foreach (var divisor in PrimeNumbers.Where(p => p <= number))
    {
      int remainder;
      do
      {
        remainder = number % divisor;
        if (remainder == 0)
        {
          number /= divisor;
          if (number == 1)
          {
            factors += $"{divisor}";
            return $"{factors}";
          }
          factors += $"{divisor} x ";
        }
      } while (remainder == 0);
    }
    return factors.Length == 0
      ? $"{number}"
      : $"{number} x {factors}";
  }
}

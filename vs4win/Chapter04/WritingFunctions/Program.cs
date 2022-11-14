using static System.Console;

TimesTable(6);

var taxToPay = CalculateTax(amount: 149, twoLetterRegionCode: "FR");
WriteLine($"You must pay {taxToPay} in tax.");

RunCardinalToOrdinal();
RunFactorial();
RunFibImperative();
RunFibFunctional();

static void TimesTable(byte number)
{
    WriteLine($"This is the {number} times table:");
    for (var row = 1; row <= 12; row++)
    {
        WriteLine(
          $"{row} x {number} = {row * number}");
    }
    WriteLine();
}

static decimal CalculateTax(
  decimal amount, string twoLetterRegionCode)
{
    var rate = twoLetterRegionCode switch
    {
        // Switzerland 
        "CH" => 0.08M,
        // Denmark 
        "DK" or "NO" => 0.25M,
        // United Kingdom
        "GB" or "FR" => 0.2M,
        // Hungary
        "HU" => 0.27M,
        // Oregon
        "OR" or "AK" or "MT" => 0.0M,
        // North Dakota
        "ND" or "WI" or "ME" or "VA" => 0.05M,
        // California
        "CA" => 0.0825M,
        // most US states 
        _ => 0.06M,
    };
    return amount * rate;
}

static void RunCardinalToOrdinal()
{
    for (var number = 1; number <= 40; number++)
    {
        Write($"{CardinalToOrdinal(number)} ");
    }
    WriteLine();
}

static int Factorial(int number)
{
    if (number < 0)
    {
        throw new ArgumentException(
          message: $"The factorial function is defined for non-negative integers only. Input: {number}",
          paramName: nameof(number));
    }
    else if (number == 0)
    {
        return 1;
    }
    else
    {
        checked // for overflow
        {
            return number * Factorial(number - 1);
        }
    }
}

static void RunFactorial()
{
    for (var i = -2; i < 15; i++)
    {
        try
        {
            WriteLine($"{i}! = {Factorial(i):N0}");
        }
        catch (System.OverflowException)
        {
            WriteLine($"{i}! is too big for a 32-bit integer.");
        }
        catch (Exception ex)
        {
            WriteLine($"{ex.GetType()}: {ex.Message}");
        }
    }
}

static int FibImperative(int term) => term switch
{
    1 => 0,
    2 => 1,
    _ => FibImperative(term - 1) + FibImperative(term - 2)
};

static void RunFibImperative()
{
    for (var i = 1; i <= 30; i++)
    {
        WriteLine("The {0} term of the Fibonacci sequence is {1:N0}.",
          arg0: CardinalToOrdinal(i),
          arg1: FibImperative(term: i));
    }
}

static int FibFunctional(int term) =>
  term switch
  {
      1 => 0,
      2 => 1,
      _ => FibFunctional(term - 1) + FibFunctional(term - 2)
  };

static void RunFibFunctional()
{
    for (var i = 1; i <= 30; i++)
    {
        WriteLine("The {0} term of the Fibonacci sequence is {1:N0}.",
          arg0: CardinalToOrdinal(i),
          arg1: FibFunctional(term: i));
    }
}

internal partial class Program
{
    /// <summary>
    /// Pass a 32-bit integer and it will be converted into its ordinal equivalent.
    /// </summary>
    /// <param name="number">Number is a cardinal value e.g. 1, 2, 3, and so on.</param>
    /// <returns>Number as an ordinal value e.g. 1st, 2nd, 3rd, and so on.</returns>
    private static string CardinalToOrdinal(int number)
    {
        switch (number)
        {
            case 11: // special cases for 11th to 13th
            case 12:
            case 13:
                return $"{number}th";
            default:
                var lastDigit = number % 10;

                var suffix = lastDigit switch
                {
                    1 => "st",
                    2 => "nd",
                    3 => "rd",
                    _ => "th"
                };
                return $"{number}{suffix}";
        }
    }
}

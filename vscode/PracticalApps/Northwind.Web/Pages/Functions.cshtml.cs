using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Northwind.Web.Pages;

public class FunctionsModel : PageModel
{
    public int? TimesTableNumberInput { get; set; }

    public decimal? Amount { get; set; }
    public string? RegionCode { get; set; }
    public decimal? TaxToPay { get; set; }

    public int? FactorialNumber { get; set; }
    public int? FactorialResult { get; set; }
    public Exception? FactorialException { get; set; }

    public int? FibonacciNumber { get; set; }
    public int? FibonacciResult { get; set; }

    public void OnGet()
    {
        // Times Table
        if (int.TryParse(HttpContext.Request.Query["timesTableNumberInput"], out int i))
        {
            TimesTableNumberInput = i;
        }

        // Calculate Tax
        if (decimal.TryParse(HttpContext.Request.Query["calculateTaxAmountInput"], out decimal amount))
        {
            Amount = amount;
            RegionCode = HttpContext.Request.Query["calculateTaxRegionCodeInput"];
            TaxToPay = CalculateTax(amount: amount, twoLetterRegionCode: RegionCode);
        }

        // Factorial
        if (int.TryParse(HttpContext.Request.Query["factorialNumberInput"], out int fact))
        {
            FactorialNumber = fact;
            try
            {
                FactorialResult = Factorial(fact);
            }
            catch (Exception ex)
            {
                FactorialException = ex;
            }
        }

        // Fibonacci
        if (int.TryParse(HttpContext.Request.Query["fibonacciNumberInput"], out int fib))
        {
            FibonacciNumber = fib;
            FibonacciResult = FibImperative(term: fib);
        }
    }

    static decimal CalculateTax(
      decimal amount, string twoLetterRegionCode)
    {
        decimal rate = 0.0M;

        // since we are matching string values a switch
        // statement is easier than a switch expression

        switch (twoLetterRegionCode)
        {
            case "CH": // Switzerland 
                rate = 0.08M;
                break;
            case "DK": // Denmark 
            case "NO": // Norway
                rate = 0.25M;
                break;
            case "GB": // United Kingdom
            case "FR": // France
                rate = 0.2M;
                break;
            case "HU": // Hungary
                rate = 0.27M;
                break;
            case "OR": // Oregon
            case "AK": // Alaska
            case "MT": // Montana
                rate = 0.0M;
                break;
            case "ND": // North Dakota
            case "WI": // Wisconsin
            case "ME": // Maine
            case "VA": // Virginia
                rate = 0.05M;
                break;
            case "CA": // California
                rate = 0.0825M;
                break;
            default: // most US states 
                rate = 0.06M;
                break;
        }

        return amount * rate;
    }

    static int Factorial(int number)
    {
        if (number < 0)
        {
            throw new ArgumentException(
              message: "The factorial function is defined for non-negative integers only.",
              paramName: "number");
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

    static int FibImperative(int term)
    {
        if (term == 1)
        {
            return 0;
        }
        else if (term == 2)
        {
            return 1;
        }
        else
        {
            return FibImperative(term - 1) + FibImperative(term - 2);
        }
    }

}

using System;
using System.Collections.Generic; // for List<T>
using System.Linq;
using static System.Console;

namespace LinqWithObjects
{
  class Program
  {
    static void LinqWithArrayOfStrings()
    {
      string[] names = new string[] { "Michael", "Pam", "Jim", "Dwight",
        "Angela", "Kevin", "Toby", "Creed" };

      // var query = names.Where(
      //   new Func<string, bool>(NameLongerThanFour));

      // var query = names.Where(NameLongerThanFour);

      IOrderedEnumerable<string> query = names
        .Where(name => name.Length > 4)
        .OrderBy(name => name.Length)
        .ThenBy(name => name);

      foreach (string item in query)
      {
        WriteLine(item);
      }
    }

    static bool NameLongerThanFour(string name)
    {
      return name.Length > 4;
    }

    static void LinqWithArrayOfExceptions()
    {
      List<Exception> errors = new()
      {
        new ArgumentException(), 
        new SystemException(),
        new IndexOutOfRangeException(), 
        new InvalidOperationException(),
        new NullReferenceException(), 
        new InvalidCastException(),
        new OverflowException(),
        new DivideByZeroException(), 
        new ApplicationException()
      };

      IEnumerable<ArithmeticException> arithmeticExceptionsQuery = 
        errors.OfType<ArithmeticException>();

      foreach (ArithmeticException exception in arithmeticExceptionsQuery)
      {
        WriteLine(exception);
      }
    }

    static void Main(string[] args)
    {
      // LinqWithArrayOfStrings();
      LinqWithArrayOfExceptions();
    }
  }
}
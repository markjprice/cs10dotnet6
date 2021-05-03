using System;
using static System.Console;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace LinqInParallel
{
  class Program
  {
    static void Main(string[] args)
    {
      Stopwatch watch = new();
      Write("Press ENTER to start: ");
      ReadLine();
      watch.Start();

      IEnumerable<int> numbers = Enumerable.Range(
        start: 1, count: 2_000_000_000);

      // int[] squares = numbers
      //   .Select(number => number * number).ToArray();

      int[] squares = numbers.AsParallel()
        .Select(number => number * number).ToArray();

      watch.Stop();
      WriteLine("{0:#,##0} elapsed milliseconds.",
        arg0: watch.ElapsedMilliseconds);
    }
  }
}
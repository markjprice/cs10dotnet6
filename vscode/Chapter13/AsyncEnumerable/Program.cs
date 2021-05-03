using System;
using System.Collections.Generic; // IAsyncEnumerable<T>
using System.Threading.Tasks; // Task
using static System.Console;

namespace AsyncEnumerable
{
  class Program
  {
    private async static IAsyncEnumerable<int> GetNumbersAsync()
    {
      Random r = new();

      // simulate work
      await Task.Delay(r.Next(1500, 3000));
      yield return r.Next(0, 1001);

      await Task.Delay(r.Next(1500, 3000));
      yield return r.Next(0, 1001);

      await Task.Delay(r.Next(1500, 3000));
      yield return r.Next(0, 1001);
    }

    static async Task Main(string[] args)
    {
      await foreach (int number in GetNumbersAsync())
      {
        WriteLine($"Number: {number}");
      }
    }
  }
}
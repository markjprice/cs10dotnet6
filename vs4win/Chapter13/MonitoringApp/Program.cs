using System;
using System.Linq;
using Packt.Shared;
using static System.Console;

namespace MonitoringApp
{
  class Program
  {
    static void Main(string[] args)
    {
      /*
      WriteLine("Processing. Please wait...");
      Recorder.Start();

      // simulate a process that requires some memory resources...
      int[] largeArrayOfInts = Enumerable.Range(1, 10_000).ToArray();

      // ...and takes some time to complete
      System.Threading.Thread.Sleep(new Random().Next(5, 10) * 1000);

      Recorder.Stop();
      */

      int[] numbers = Enumerable.Range(1, 50_000).ToArray();

      WriteLine("Using string with +");
      Recorder.Start();
      string s = "";
      for (int i = 0; i < numbers.Length; i++)
      {
        s += numbers[i] + ", ";
      }
      Recorder.Stop();

      WriteLine("Using StringBuilder");
      Recorder.Start();
      var builder = new System.Text.StringBuilder();
      for (int i = 0; i < numbers.Length; i++)
      {
        builder.Append(numbers[i]); builder.Append(", ");
      }
      Recorder.Stop();
    }
  }
}
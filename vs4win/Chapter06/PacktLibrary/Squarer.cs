using System;
using System.Threading;

namespace Packt.Shared
{
  public static class Squarer
  {
    public static double Square<T>(T input)
      where T : IConvertible
    {
      // convert using the current culture
      double d = input.ToDouble(
        Thread.CurrentThread.CurrentCulture);

      return d * d;
    }
  }
}
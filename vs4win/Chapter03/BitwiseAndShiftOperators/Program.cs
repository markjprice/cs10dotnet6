using System;
using static System.Console;

namespace BitwiseAndShiftOperators
{
  class Program
  {
    static string ToBinaryString(int value)
    {
      return Convert.ToString(value, toBase: 2).PadLeft(8, '0');
    }

    static void Main(string[] args)
    {
      int a = 10; // 00001010
      int b = 6;  // 00000110

      WriteLine($"a = {a}");
      WriteLine($"b = {b}");
      WriteLine($"a & b = {a & b}"); // 2-bit column only
      WriteLine($"a | b = {a | b}"); // 8, 4, and 2-bit columns
      WriteLine($"a ^ b = {a ^ b}"); // 8 and 4-bit columns

      WriteLine($"a =     {ToBinaryString(a)}");
      WriteLine($"b =     {ToBinaryString(b)}");
      WriteLine($"a & b = {ToBinaryString(a & b)}");
      WriteLine($"a | b = {ToBinaryString(a | b)}");
      WriteLine($"a ^ b = {ToBinaryString(a ^ b)}");

      // 01010000 left-shift a by three bit columns
      WriteLine($"a << 3 = {a << 3}");

      // multiply a by 8
      WriteLine($"a * 8 = {a * 8}");

      // 00000011 right-shift b by one bit column
      WriteLine($"b >> 1 = {b >> 1}");
    }
  }
}
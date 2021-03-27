using System;
using static System.Console;

namespace WorkingWithRanges
{
  class Program
  {
    static void Main(string[] args)
    {
      string name = "Samantha Jones";

      // Using Substring

      int lengthOfFirst = name.IndexOf(' ');
      int lengthOfLast = name.Length - lengthOfFirst - 1;

      string firstName = name.Substring(
        startIndex: 0,
        length: lengthOfFirst);

      string lastName = name.Substring(
        startIndex: name.Length - lengthOfLast,
        length: lengthOfLast);

      WriteLine($"First name: {firstName}, Last name: {lastName}");

      // Using spans

      ReadOnlySpan<char> nameAsSpan = name.AsSpan();
      var firstNameSpan = nameAsSpan[0..lengthOfFirst];
      var lastNameSpan = nameAsSpan[^lengthOfLast..^0];

      WriteLine("First name: {0}, Last name: {1}",
        arg0: firstNameSpan.ToString(),
        arg1: lastNameSpan.ToString());
    }
  }
}
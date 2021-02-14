using System;
using static System.Console;
using System.IO;

namespace SelectionStatements
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length == 0)
      {
        WriteLine("There are no arguments.");
      }
      else
      {
        WriteLine("There is at least one argument.");
      }

      // add and remove the "" to change the behavior
      object o = 3;

      int j = 4;

      if (o is int i)
      {
        WriteLine($"{i} x {j} = {i * j}");
      }
      else
      {
        WriteLine("o is not an int so it cannot multiply!");
      }

    A_label:
      var number = (new Random()).Next(1, 7);

      WriteLine($"My random number is {number}");

      switch (number)
      {
        case 1:
          WriteLine("One");
          break; // jumps to end of switch statement 
        case 2:
          WriteLine("Two");
          goto case 1;
        case 3:
        case 4:
          WriteLine("Three or four");
          goto case 1;
        case 5:
          // go to sleep for half a second
          System.Threading.Thread.Sleep(500);
          goto A_label;
        default:
          WriteLine("Default");
          break;
      } // end of switch statement

      string path = "/Users/markjprice/Code/Chapter03";
      // string path = @"C:\Code\Chapter03";

      Write("Press R for readonly or W for write: ");
      ConsoleKeyInfo key = ReadKey();
      WriteLine();

      Stream s = null;

      if (key.Key == ConsoleKey.R)
      {
        s = File.Open(
          Path.Combine(path, "file.txt"),
          FileMode.OpenOrCreate,
          FileAccess.Read);
      }
      else
      {
        s = File.Open(
          Path.Combine(path, "file.txt"),
          FileMode.OpenOrCreate,
          FileAccess.Write);
      }

      string message = string.Empty;

      switch (s)
      {
        case FileStream writeableFile when s.CanWrite:
          message = "The stream is a file that I can write to.";
          break;
        case FileStream readOnlyFile:
          message = "The stream is a read-only file.";
          break;
        case MemoryStream ms:
          message = "The stream is a memory address.";
          break;
        default: // always evaluated last despite its current position
          message = "The stream is some other type.";
          break;
        case null:
          message = "The stream is null.";
          break;
      }

      WriteLine(message);

      message = s switch
      {
        FileStream writeableFile when s.CanWrite
          => "The stream is a file that I can write to.",
        FileStream readOnlyFile
          => "The stream is a read-only file.",
        MemoryStream ms
          => "The stream is a memory address.",
        null
          => "The stream is null.",
        _
          => "The stream is some other type."
      };

      WriteLine(message);
    }
  }
}

using System;

namespace Arrays
{
  class Program
  {
    static void Main(string[] args)
    {
      string[] names; // can reference any array of strings

      // allocating memory for four strings in an array
      names = new string[4];

      // storing items at index positions
      names[0] = "Kate";
      names[1] = "Jack";
      names[2] = "Rebecca";
      names[3] = "Tom";

      // looping through the names
      for (int i = 0; i < names.Length; i++)
      {
        // output the item at index position i
        Console.WriteLine(names[i]);
      }
    }
  }
}
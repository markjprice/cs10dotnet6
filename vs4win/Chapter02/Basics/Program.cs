using System;
using System.Linq;
using System.Reflection;

// #error version

namespace Basics
{
  class Program
  {
    static void Main(string[] args)
    {
      // declare some unused variables using types
      // in additional assemblies
      System.Data.DataSet ds;
      System.Net.Http.HttpClient client;

      // loop through the assemblies that this app references 
      foreach (AssemblyName namedAssembly in Assembly
        .GetEntryAssembly().GetReferencedAssemblies())
      {
        // load the assembly so we can read its details
        var a = Assembly.Load(namedAssembly);

        // declare a variable to count the number of methods
        int methodCount = 0;

        // loop through all the types in the assembly 
        foreach (TypeInfo type in a.DefinedTypes)
        {
          // add up the counts of methods 
          methodCount += type.GetMethods().Length;
        }
        
        // output the count of types and their methods
        Console.WriteLine(
          "{0:N0} types with {1:N0} methods in {2} assembly.",
          arg0: a.DefinedTypes.Count(),
          arg1: methodCount,
          arg2: namedAssembly.Name);
      }
    }
  }
}
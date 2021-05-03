using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

namespace NestedAndChildTasks
{
  class Program
  {
    private static void OuterMethod()
    {
      WriteLine("Outer method starting...");
      Task innerTask = Task.Factory.StartNew(InnerMethod,
        TaskCreationOptions.AttachedToParent);
      WriteLine("Outer method finished.");
    }

    private static void InnerMethod()
    {
      WriteLine("Inner method starting...");
      Thread.Sleep(2000);
      WriteLine("Inner method finished.");
    }

    static void Main(string[] args)
    {
      Task outerTask = Task.Factory.StartNew(OuterMethod);
      outerTask.Wait();
      WriteLine("Console app is stopping.");
    }
  }
}
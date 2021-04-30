using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Instrumenting
{
  class Program
  {
    static void Main(string[] args)
    {
      // write to a text file in the project folder
      Trace.Listeners.Add(new TextWriterTraceListener(
        File.CreateText(Path.Combine(Environment.GetFolderPath(
          Environment.SpecialFolder.DesktopDirectory), "log.txt"))));

      // text writer is buffered, so this option calls
      // Flush() on all listeners after writing
      Trace.AutoFlush = true;

      Debug.WriteLine("Debug says, I am watching!");
      Trace.WriteLine("Trace says, I am watching!");

      ConfigurationBuilder builder = new()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json",
          optional: true, reloadOnChange: true);

      IConfigurationRoot configuration = builder.Build();

      TraceSwitch ts = new(
        displayName: "PacktSwitch",
        description: "This switch is set via a JSON config.");

      configuration.GetSection("PacktSwitch").Bind(ts);

      Trace.WriteLineIf(ts.TraceError, "Trace error");
      Trace.WriteLineIf(ts.TraceWarning, "Trace warning");
      Trace.WriteLineIf(ts.TraceInfo, "Trace information");
      Trace.WriteLineIf(ts.TraceVerbose, "Trace verbose");
    }
  }
}
using System;
using System.Net.Http; // HttpClient
using System.Threading.Tasks; // Task

using static System.Console;

namespace AsyncConsole
{
  class Program
  {
    static async Task Main(string[] args)
    {
      HttpClient client = new();

      HttpResponseMessage response =
        await client.GetAsync("http://www.apple.com/");

      WriteLine("Apple's home page has {0:N0} bytes.",
        response.Content.Headers.ContentLength);
    }
  }
}
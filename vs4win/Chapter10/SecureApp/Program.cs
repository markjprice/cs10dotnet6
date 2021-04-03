using static System.Console;
using Packt.Shared;
using System.Threading;
using System.Security;
using System.Security.Principal;
using System.Security.Claims;
using System.Collections.Generic;

namespace SecureApp
{
  class Program
  {
    static void Main(string[] args)
    {
      // register three users

      Protector.Register(
        username: "Alice", 
        password: "Pa$$w0rd", 
        roles: new[] { "Admins" });

      Protector.Register("Bob", "Pa$$w0rd",
        new[] { "Sales", "TeamLeads" });

      Protector.Register("Eve", "Pa$$w0rd");

      // prompt user to enter username and password to login
      // as one of these three users

      Write($"Enter your user name: ");
      string username = ReadLine();

      Write($"Enter your password: ");
      string password = ReadLine();

      Protector.LogIn(username, password);

      if (Thread.CurrentPrincipal == null)
      {
        WriteLine("Log in failed.");
        return;
      }

      IPrincipal p = Thread.CurrentPrincipal;

      WriteLine($"IsAuthenticated: {p.Identity.IsAuthenticated}");
      WriteLine($"AuthenticationType: {p.Identity.AuthenticationType}");
      WriteLine($"Name: {p.Identity.Name}");
      WriteLine($"IsInRole(\"Admins\"): {p.IsInRole("Admins")}");
      WriteLine($"IsInRole(\"Sales\"): {p.IsInRole("Sales")}");

      if (p is ClaimsPrincipal)
      {
        WriteLine($"{p.Identity.Name} has the following claims:");

        IEnumerable<Claim> claims = (p as ClaimsPrincipal).Claims;

        foreach (Claim claim in claims)
        {
          WriteLine($"{claim.Type}: {claim.Value}");
        }
      }

      try
      {
        SecureFeature();
      }
      catch (System.Exception ex)
      {
        WriteLine($"{ex.GetType()}: {ex.Message}");
      }
    }

    static void SecureFeature()
    {
      if (Thread.CurrentPrincipal == null)
      {
        throw new SecurityException(
          "A user must be logged in to access this feature.");
      }

      if (!Thread.CurrentPrincipal.IsInRole("Admins"))
      {
        throw new SecurityException(
          "User must be a member of Admins to access this feature.");
      }

      WriteLine("You have access to this secure feature.");
    }
  }
}
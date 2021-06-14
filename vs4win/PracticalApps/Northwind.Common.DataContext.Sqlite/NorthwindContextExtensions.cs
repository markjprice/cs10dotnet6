using Microsoft.EntityFrameworkCore; // UseSqlite
using Microsoft.Extensions.DependencyInjection; // IServiceCollection
using System.IO; // Path

namespace Packt.Shared
{
  public static class NorthwindContextExtensions
  {
    /// <summary>
    /// Adds NorthwindContext to the specified IServiceCollection. Uses the Sqlite database provider.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>An IServiceCollection that can be used to add more services.</returns>
    public static IServiceCollection AddNorthwindContext(this IServiceCollection services)
    {
      string databasePath = Path.Combine("..", "Northwind.db");

      services.AddDbContext<NorthwindContext>(options =>
        options.UseSqlite($"Data Source={databasePath}")
        .UseLoggerFactory(new ConsoleLoggerFactory())
      );

      return services;
    }
  }
}
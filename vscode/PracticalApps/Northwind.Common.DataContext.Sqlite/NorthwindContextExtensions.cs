using Microsoft.EntityFrameworkCore; // UseSqlite
using Microsoft.Extensions.DependencyInjection; // IServiceCollection
using System.IO; // Path

namespace Packt.Shared
{
  public static class NorthwindContextExtensions
  {
    public static IServiceCollection AddNorthwindContext(this IServiceCollection services)
    {
      string databasePath = Path.Combine("..", "Northwind.db");

      services.AddDbContext<NorthwindContext>(options =>
        options.UseSqlite($"Data Source={databasePath}"));

      return services;
    }
  }
}
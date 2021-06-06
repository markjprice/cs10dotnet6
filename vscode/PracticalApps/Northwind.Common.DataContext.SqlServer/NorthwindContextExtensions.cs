using Microsoft.EntityFrameworkCore; // UseSqlServer
using Microsoft.Extensions.DependencyInjection; // IServiceCollection

namespace Packt.Shared
{
  public static class NorthwindContextExtensions
  {
    public static IServiceCollection AddNorthwindContext(
      this IServiceCollection services)
    {
      string connectionString = "Data Source=.;Initial Catalog=Northwind;"
        + "Integrated Security=true;MultipleActiveResultsets=true;";

      services.AddDbContext<NorthwindContext>(options =>
        options.UseSqlServer(connectionString));

      return services;
    }
  }
}
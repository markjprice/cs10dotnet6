using Microsoft.EntityFrameworkCore;
using static System.Environment;
using static System.IO.Path;

namespace Packt.Shared
{
  // this manages the connection to the database 
  public class Northwind : DbContext
  {
    // these properties map to tables in the database 
    public DbSet<Customer> Customers { get; set; }
    
    protected override void OnConfiguring(
      DbContextOptionsBuilder optionsBuilder)
    {
      string path = Combine(CurrentDirectory, "Northwind.db");
        
      optionsBuilder.UseSqlite($"Filename={path}");
    }
  }
}
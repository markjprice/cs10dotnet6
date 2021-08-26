using Microsoft.EntityFrameworkCore; // DbContext, DbSet<T>

namespace Packt.Shared;

// this manages the connection to the database 
public class Northwind : DbContext
{
  // these properties map to tables in the database 
  public DbSet<Category>? Categories { get; set; }
  public DbSet<Product>? Products { get; set; }

  protected override void OnConfiguring(
    DbContextOptionsBuilder optionsBuilder)
  {
    string path = Path.Combine(
      Environment.CurrentDirectory, "Northwind.db");

    // optionsBuilder.UseSqlite($"Filename={path}");

    string connection = "Data Source=.;" +
        "Initial Catalog=Northwind;" +
        "Integrated Security=true;" +
        "MultipleActiveResultSets=true;";

    optionsBuilder.UseSqlServer(connection);
  }

  protected override void OnModelCreating(
    ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Product>()
      .Property(product => product.UnitPrice)
      .HasConversion<double>();
  }
}

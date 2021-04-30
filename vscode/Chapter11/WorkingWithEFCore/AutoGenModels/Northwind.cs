using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WorkingWithEFCore.AutoGen
{
  public partial class Northwind : DbContext
  {
    public Northwind()
    {
    }

    public Northwind(DbContextOptions<Northwind> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        optionsBuilder.UseSqlite("Filename=Northwind.db");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Category>(entity =>
      {
        entity.Property(e => e.CategoryId)
                  .ValueGeneratedNever()
                  .HasColumnName("CategoryID");

        entity.Property(e => e.CategoryName).HasAnnotation("Relational:ColumnType", "nvarchar (15)");

        entity.Property(e => e.Description).HasAnnotation("Relational:ColumnType", "ntext");

        entity.Property(e => e.Picture).HasAnnotation("Relational:ColumnType", "image");
      });

      modelBuilder.Entity<Product>(entity =>
      {
        entity.Property(e => e.ProductId)
                  .ValueGeneratedNever()
                  .HasColumnName("ProductID");

        entity.Property(e => e.CategoryId)
                  .HasColumnName("CategoryID")
                  .HasAnnotation("Relational:ColumnType", "int");

        entity.Property(e => e.Discontinued)
                  .HasDefaultValueSql("0")
                  .HasAnnotation("Relational:ColumnType", "bit");

        entity.Property(e => e.ProductName).HasAnnotation("Relational:ColumnType", "nvarchar (40)");

        entity.Property(e => e.QuantityPerUnit).HasAnnotation("Relational:ColumnType", "nvarchar (20)");

        entity.Property(e => e.ReorderLevel)
                  .HasDefaultValueSql("0")
                  .HasAnnotation("Relational:ColumnType", "smallint");

        entity.Property(e => e.SupplierId)
                  .HasColumnName("SupplierID")
                  .HasAnnotation("Relational:ColumnType", "int");

        entity.Property(e => e.UnitPrice)
                  .HasDefaultValueSql("0")
                  .HasAnnotation("Relational:ColumnType", "money");

        entity.Property(e => e.UnitsInStock)
                  .HasDefaultValueSql("0")
                  .HasAnnotation("Relational:ColumnType", "smallint");

        entity.Property(e => e.UnitsOnOrder)
                  .HasDefaultValueSql("0")
                  .HasAnnotation("Relational:ColumnType", "smallint");
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}

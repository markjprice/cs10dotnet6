using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Packt.Shared
{
  public partial class NorthwindContext : DbContext
  {
    public NorthwindContext()
    {
    }

    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlphabeticalListOfProduct> AlphabeticalListOfProducts { get; set; } = null!;
    public virtual DbSet<Category> Categories { get; set; } = null!;
    public virtual DbSet<CategorySalesFor1997> CategorySalesFor1997s { get; set; } = null!;
    public virtual DbSet<CurrentProductList> CurrentProductLists { get; set; } = null!;
    public virtual DbSet<Customer> Customers { get; set; } = null!;
    public virtual DbSet<CustomerAndSuppliersByCity> CustomerAndSuppliersByCities { get; set; } = null!;
    public virtual DbSet<CustomerCustomerDemo> CustomerCustomerDemos { get; set; } = null!;
    public virtual DbSet<CustomerDemographic> CustomerDemographics { get; set; } = null!;
    public virtual DbSet<Employee> Employees { get; set; } = null!;
    public virtual DbSet<EmployeeTerritory> EmployeeTerritories { get; set; } = null!;
    public virtual DbSet<Invoice> Invoices { get; set; } = null!;
    public virtual DbSet<Order> Orders { get; set; } = null!;
    public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
    public virtual DbSet<OrderDetailsExtended> OrderDetailsExtendeds { get; set; } = null!;
    public virtual DbSet<OrderSubtotal> OrderSubtotals { get; set; } = null!;
    public virtual DbSet<OrdersQry> OrdersQries { get; set; } = null!;
    public virtual DbSet<Product> Products { get; set; } = null!;
    public virtual DbSet<ProductSalesFor1997> ProductSalesFor1997s { get; set; } = null!;
    public virtual DbSet<ProductsAboveAveragePrice> ProductsAboveAveragePrices { get; set; } = null!;
    public virtual DbSet<ProductsByCategory> ProductsByCategories { get; set; } = null!;
    public virtual DbSet<QuarterlyOrder> QuarterlyOrders { get; set; } = null!;
    public virtual DbSet<Region> Regions { get; set; } = null!;
    public virtual DbSet<SalesByCategory> SalesByCategories { get; set; } = null!;
    public virtual DbSet<SalesTotalsByAmount> SalesTotalsByAmounts { get; set; } = null!;
    public virtual DbSet<Shipper> Shippers { get; set; } = null!;
    public virtual DbSet<SummaryOfSalesByQuarter> SummaryOfSalesByQuarters { get; set; } = null!;
    public virtual DbSet<SummaryOfSalesByYear> SummaryOfSalesByYears { get; set; } = null!;
    public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
    public virtual DbSet<Territory> Territories { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Northwind;Integrated Security=true;");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

      modelBuilder.Entity<AlphabeticalListOfProduct>(entity =>
      {
        entity.ToView("Alphabetical list of products");
      });

      modelBuilder.Entity<CategorySalesFor1997>(entity =>
      {
        entity.ToView("Category Sales for 1997");
      });

      modelBuilder.Entity<CurrentProductList>(entity =>
      {
        entity.ToView("Current Product List");

        entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
      });

      modelBuilder.Entity<Customer>(entity =>
      {
        entity.Property(e => e.CustomerId).IsFixedLength();
      });

      modelBuilder.Entity<CustomerAndSuppliersByCity>(entity =>
      {
        entity.ToView("Customer and Suppliers by City");
      });

      modelBuilder.Entity<CustomerCustomerDemo>(entity =>
      {
        entity.HasKey(e => new { e.CustomerId, e.CustomerTypeId })
                  .IsClustered(false);

        entity.Property(e => e.CustomerId).IsFixedLength();

        entity.Property(e => e.CustomerTypeId).IsFixedLength();

        entity.HasOne(d => d.Customer)
                  .WithMany(p => p.CustomerCustomerDemos)
                  .HasForeignKey(d => d.CustomerId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_CustomerCustomerDemo_Customers");

        entity.HasOne(d => d.CustomerType)
                  .WithMany(p => p.CustomerCustomerDemos)
                  .HasForeignKey(d => d.CustomerTypeId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_CustomerCustomerDemo");
      });

      modelBuilder.Entity<CustomerDemographic>(entity =>
      {
        entity.HasKey(e => e.CustomerTypeId)
                  .IsClustered(false);

        entity.Property(e => e.CustomerTypeId).IsFixedLength();
      });

      modelBuilder.Entity<Employee>(entity =>
      {
        entity.HasOne(d => d.ReportsToNavigation)
                  .WithMany(p => p.InverseReportsToNavigation)
                  .HasForeignKey(d => d.ReportsTo)
                  .HasConstraintName("FK_Employees_Employees");
      });

      modelBuilder.Entity<EmployeeTerritory>(entity =>
      {
        entity.HasKey(e => new { e.EmployeeId, e.TerritoryId })
                  .IsClustered(false);

        entity.HasOne(d => d.Employee)
                  .WithMany(p => p.EmployeeTerritories)
                  .HasForeignKey(d => d.EmployeeId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_EmployeeTerritories_Employees");

        entity.HasOne(d => d.Territory)
                  .WithMany(p => p.EmployeeTerritories)
                  .HasForeignKey(d => d.TerritoryId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_EmployeeTerritories_Territories");
      });

      modelBuilder.Entity<Invoice>(entity =>
      {
        entity.ToView("Invoices");

        entity.Property(e => e.CustomerId).IsFixedLength();
      });

      modelBuilder.Entity<Order>(entity =>
      {
        entity.Property(e => e.CustomerId).IsFixedLength();

        entity.Property(e => e.Freight).HasDefaultValueSql("((0))");

        entity.HasOne(d => d.Customer)
                  .WithMany(p => p.Orders)
                  .HasForeignKey(d => d.CustomerId)
                  .HasConstraintName("FK_Orders_Customers");

        entity.HasOne(d => d.Employee)
                  .WithMany(p => p.Orders)
                  .HasForeignKey(d => d.EmployeeId)
                  .HasConstraintName("FK_Orders_Employees");

        entity.HasOne(d => d.ShipViaNavigation)
                  .WithMany(p => p.Orders)
                  .HasForeignKey(d => d.ShipVia)
                  .HasConstraintName("FK_Orders_Shippers");
      });

      modelBuilder.Entity<OrderDetail>(entity =>
      {
        entity.HasKey(e => new { e.OrderId, e.ProductId })
                  .HasName("PK_Order_Details");

        entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

        entity.HasOne(d => d.Order)
                  .WithMany(p => p.OrderDetails)
                  .HasForeignKey(d => d.OrderId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Order_Details_Orders");

        entity.HasOne(d => d.Product)
                  .WithMany(p => p.OrderDetails)
                  .HasForeignKey(d => d.ProductId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Order_Details_Products");
      });

      modelBuilder.Entity<OrderDetailsExtended>(entity =>
      {
        entity.ToView("Order Details Extended");
      });

      modelBuilder.Entity<OrderSubtotal>(entity =>
      {
        entity.ToView("Order Subtotals");
      });

      modelBuilder.Entity<OrdersQry>(entity =>
      {
        entity.ToView("Orders Qry");

        entity.Property(e => e.CustomerId).IsFixedLength();
      });

      modelBuilder.Entity<Product>(entity =>
      {
        entity.Property(e => e.ReorderLevel).HasDefaultValueSql("((0))");

        entity.Property(e => e.UnitPrice).HasDefaultValueSql("((0))");

        entity.Property(e => e.UnitsInStock).HasDefaultValueSql("((0))");

        entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");

        entity.HasOne(d => d.Category)
                  .WithMany(p => p.Products)
                  .HasForeignKey(d => d.CategoryId)
                  .HasConstraintName("FK_Products_Categories");

        entity.HasOne(d => d.Supplier)
                  .WithMany(p => p.Products)
                  .HasForeignKey(d => d.SupplierId)
                  .HasConstraintName("FK_Products_Suppliers");
      });

      modelBuilder.Entity<ProductSalesFor1997>(entity =>
      {
        entity.ToView("Product Sales for 1997");
      });

      modelBuilder.Entity<ProductsAboveAveragePrice>(entity =>
      {
        entity.ToView("Products Above Average Price");
      });

      modelBuilder.Entity<ProductsByCategory>(entity =>
      {
        entity.ToView("Products by Category");
      });

      modelBuilder.Entity<QuarterlyOrder>(entity =>
      {
        entity.ToView("Quarterly Orders");

        entity.Property(e => e.CustomerId).IsFixedLength();
      });

      modelBuilder.Entity<Region>(entity =>
      {
        entity.HasKey(e => e.RegionId)
                  .IsClustered(false);

        entity.Property(e => e.RegionId).ValueGeneratedNever();

        entity.Property(e => e.RegionDescription).IsFixedLength();
      });

      modelBuilder.Entity<SalesByCategory>(entity =>
      {
        entity.ToView("Sales by Category");
      });

      modelBuilder.Entity<SalesTotalsByAmount>(entity =>
      {
        entity.ToView("Sales Totals by Amount");
      });

      modelBuilder.Entity<SummaryOfSalesByQuarter>(entity =>
      {
        entity.ToView("Summary of Sales by Quarter");
      });

      modelBuilder.Entity<SummaryOfSalesByYear>(entity =>
      {
        entity.ToView("Summary of Sales by Year");
      });

      modelBuilder.Entity<Territory>(entity =>
      {
        entity.HasKey(e => e.TerritoryId)
                  .IsClustered(false);

        entity.Property(e => e.TerritoryDescription).IsFixedLength();

        entity.HasOne(d => d.Region)
                  .WithMany(p => p.Territories)
                  .HasForeignKey(d => d.RegionId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Territories_Region");
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}

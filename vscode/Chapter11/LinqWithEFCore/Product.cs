using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

public class Product
{
  public int ProductId { get; set; }

  [Required]
  [StringLength(40)]
  public string ProductName { get; set; } = null!;

  public int? SupplierId { get; set; }

  public int? CategoryId { get; set; }

  [StringLength(20)]
  public string? QuantityPerUnit { get; set; }

  [Column(TypeName = "money")] // required for SQL Server provider
  public decimal? UnitPrice { get; set; }

  public short? UnitsInStock { get; set; }

  public short? UnitsOnOrder { get; set; }

  public short? ReorderLevel { get; set; }

  public bool Discontinued { get; set; }
}

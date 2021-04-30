using System.ComponentModel.DataAnnotations;

namespace Packt.Shared
{
  public class Product
  {
    public int ProductID { get; set; } 

    [Required]
    [StringLength(40)]
    public string ProductName { get; set; }

    public int? SupplierID { get; set; }

    public int? CategoryID { get; set; } 

    [StringLength(20)]
    public string QuantityPerUnit { get; set; } 

    public decimal? UnitPrice { get; set; } 

    public short? UnitsInStock { get; set; } 

    public short? UnitsOnOrder { get; set; } 

    public short? ReorderLevel { get; set; } 

    public bool Discontinued { get; set; }
  }
}
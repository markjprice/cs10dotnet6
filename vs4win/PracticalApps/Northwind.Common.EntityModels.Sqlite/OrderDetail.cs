using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
  [Table("Order Details")]
  [Index(nameof(OrderId), Name = "OrderId")]
  [Index(nameof(OrderId), Name = "OrdersOrder_Details")]
  [Index(nameof(ProductId), Name = "ProductId")]
  [Index(nameof(ProductId), Name = "ProductsOrder_Details")]
  public partial class OrderDetail
  {
    [Key]
    [Column(TypeName = "int")]
    public int OrderId { get; set; }

    [Key]
    [Column(TypeName = "int")]
    public int ProductId { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal? UnitPrice { get; set; } = null!;

    [Column(TypeName = "smallint")]
    public short Quantity { get; set; }

    [Column(TypeName = "real")]
    public double Discount { get; set; }

    [ForeignKey(nameof(OrderId))]
    [InverseProperty("OrderDetails")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey(nameof(ProductId))]
    [InverseProperty("OrderDetails")]
    public virtual Product Product { get; set; } = null!;
  }
}

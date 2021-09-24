using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    [Index(nameof(CustomerId), Name = "CustomerId")]
    [Index(nameof(CustomerId), Name = "CustomersOrders")]
    [Index(nameof(EmployeeId), Name = "EmployeeId")]
    [Index(nameof(EmployeeId), Name = "EmployeesOrders")]
    [Index(nameof(OrderDate), Name = "OrderDate")]
    [Index(nameof(ShipPostalCode), Name = "ShipPostalCode")]
    [Index(nameof(ShippedDate), Name = "ShippedDate")]
    [Index(nameof(ShipVia), Name = "ShippersOrders")]
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int OrderId { get; set; }
        [StringLength(5)]
        public string? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OrderDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RequiredDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        [Column(TypeName = "money")]
        public decimal? Freight { get; set; }
        [StringLength(40)]
        public string? ShipName { get; set; }
        [StringLength(60)]
        public string? ShipAddress { get; set; }
        [StringLength(15)]
        public string? ShipCity { get; set; }
        [StringLength(15)]
        public string? ShipRegion { get; set; }
        [StringLength(10)]
        public string? ShipPostalCode { get; set; }
        [StringLength(15)]
        public string? ShipCountry { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Orders")]
        public virtual Customer? Customer { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("Orders")]
        public virtual Employee? Employee { get; set; }
        [ForeignKey(nameof(ShipVia))]
        [InverseProperty(nameof(Shipper.Orders))]
        public virtual Shipper? ShipViaNavigation { get; set; }
        [InverseProperty(nameof(OrderDetail.Order))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

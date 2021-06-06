using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Packt.Shared
{
    [Keyless]
    public partial class SalesTotalsByAmount
    {
        [Column(TypeName = "money")]
        public decimal? SaleAmount { get; set; }
        [Column("OrderID")]
        public int OrderId { get; set; }
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ShippedDate { get; set; }
    }
}

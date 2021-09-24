using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    [Keyless]
    public partial class SalesByCategory
    {
        [Column("CategoryID")]
        public int CategoryId { get; set; }
        [StringLength(15)]
        public string CategoryName { get; set; } = null!;
        [StringLength(40)]
        public string ProductName { get; set; } = null!;
        [Column(TypeName = "money")]
        public decimal? ProductSales { get; set; }
    }
}

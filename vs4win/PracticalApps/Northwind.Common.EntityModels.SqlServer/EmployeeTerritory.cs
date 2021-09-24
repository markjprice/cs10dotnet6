using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    public partial class EmployeeTerritory
    {
        [Key]
        [Column("EmployeeID")]
        public int EmployeeId { get; set; }
        [Key]
        [Column("TerritoryID")]
        [StringLength(20)]
        public string TerritoryId { get; set; } = null!;

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("EmployeeTerritories")]
        public virtual Employee Employee { get; set; } = null!;
        [ForeignKey(nameof(TerritoryId))]
        [InverseProperty("EmployeeTerritories")]
        public virtual Territory Territory { get; set; } = null!;
    }
}

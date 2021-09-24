using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    public partial class Territory
    {
        public Territory()
        {
            EmployeeTerritories = new HashSet<EmployeeTerritory>();
        }

        [Key]
        [Column("TerritoryID")]
        [StringLength(20)]
        public string TerritoryId { get; set; } = null!;
        [StringLength(50)]
        public string TerritoryDescription { get; set; } = null!;
        [Column("RegionID")]
        public int RegionId { get; set; }

        [ForeignKey(nameof(RegionId))]
        [InverseProperty("Territories")]
        public virtual Region Region { get; set; } = null!;
        [InverseProperty(nameof(EmployeeTerritory.Territory))]
        public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }
    }
}

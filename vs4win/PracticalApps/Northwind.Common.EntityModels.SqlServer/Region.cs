using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    [Table("Region")]
    public partial class Region
    {
        public Region()
        {
            Territories = new HashSet<Territory>();
        }

        [Key]
        [Column("RegionID")]
        public int RegionId { get; set; }
        [StringLength(50)]
        public string RegionDescription { get; set; } = null!;

        [InverseProperty(nameof(Territory.Region))]
        public virtual ICollection<Territory> Territories { get; set; }
    }
}

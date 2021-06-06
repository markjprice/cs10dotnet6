using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Packt.Shared
{
    [Keyless]
    public partial class Territory
    {
        [Required]
        [Column(TypeName = "nvarchar")]
        public string TerritoryId { get; set; }
        [Required]
        [Column(TypeName = "nchar")]
        public string TerritoryDescription { get; set; }
        [Column(TypeName = "int")]
        public long RegionId { get; set; }
    }
}

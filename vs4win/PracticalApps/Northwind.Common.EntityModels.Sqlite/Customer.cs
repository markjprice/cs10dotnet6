using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization; // [XmlIgnore]
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
  [Index(nameof(City), Name = "City")]
  [Index(nameof(CompanyName), Name = "CompanyNameCustomers")]
  [Index(nameof(PostalCode), Name = "PostalCodeCustomers")]
  [Index(nameof(Region), Name = "Region")]
  public partial class Customer
  {
    public Customer()
    {
      Orders = new HashSet<Order>();
    }

    [Key]
    [Column(TypeName = "nchar (5)")]
    [StringLength(5)]
    [RegularExpression("[A-Z]{5}")]
    public string CustomerId { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar (40)")]
    [StringLength(40)]
    public string CompanyName { get; set; } = null!;

    [Column(TypeName = "nvarchar (30)")]
    [StringLength(30)]
    public string? ContactName { get; set; }

    [Column(TypeName = "nvarchar (30)")]
    [StringLength(30)]
    public string? ContactTitle { get; set; }

    [Column(TypeName = "nvarchar (60)")]
    [StringLength(60)]
    public string? Address { get; set; }

    [Column(TypeName = "nvarchar (15)")]
    [StringLength(15)]
    public string? City { get; set; }

    [Column(TypeName = "nvarchar (15)")]
    [StringLength(15)]
    public string? Region { get; set; }

    [Column(TypeName = "nvarchar (10)")]
    [StringLength(10)]
    public string? PostalCode { get; set; }

    [Column(TypeName = "nvarchar (15)")]
    [StringLength(15)]
    public string? Country { get; set; }

    [Column(TypeName = "nvarchar (24)")]
    [StringLength(24)]
    public string? Phone { get; set; }

    [Column(TypeName = "nvarchar (24)")]
    [StringLength(24)]
    public string? Fax { get; set; }

    [InverseProperty(nameof(Order.Customer))]
    [XmlIgnore]
    public virtual ICollection<Order> Orders { get; set; }
  }
}

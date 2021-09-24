using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization; // [XmlIgnore]

namespace Packt.Shared
{
  [Index(nameof(City), Name = "City")]
  [Index(nameof(CompanyName), Name = "CompanyName")]
  [Index(nameof(PostalCode), Name = "PostalCode")]
  [Index(nameof(Region), Name = "Region")]
  public partial class Customer
  {
    public Customer()
    {
      CustomerCustomerDemos = new HashSet<CustomerCustomerDemo>();
      Orders = new HashSet<Order>();
    }

    [Key]
    [StringLength(5)]
    [RegularExpression("[A-Z]{5}")]
    [Required]
    public string CustomerId { get; set; } = null!;

    [StringLength(40)]
    [Required]
    public string CompanyName { get; set; } = null!;

    [StringLength(30)]
    public string? ContactName { get; set; }

    [StringLength(30)]
    public string? ContactTitle { get; set; }

    [StringLength(60)]
    public string? Address { get; set; }

    [StringLength(15)]
    public string? City { get; set; }

    [StringLength(15)]
    public string? Region { get; set; }

    [StringLength(10)]
    public string? PostalCode { get; set; }

    [StringLength(15)]
    public string? Country { get; set; }

    [StringLength(24)]
    public string? Phone { get; set; }

    [StringLength(24)]
    public string? Fax { get; set; }

    [InverseProperty(nameof(CustomerCustomerDemo.Customer))]
    [XmlIgnore]
    public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }

    [InverseProperty(nameof(Order.Customer))]
    [XmlIgnore]
    public virtual ICollection<Order> Orders { get; set; }
  }
}

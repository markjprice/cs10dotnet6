using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
  [Index(nameof(LastName), Name = "LastName")]
  [Index(nameof(PostalCode), Name = "PostalCodeEmployees")]
  public partial class Employee
  {
    public Employee()
    {
      Orders = new HashSet<Order>();
    }

    [Key]
    public int EmployeeId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar (20)")]
    [StringLength(20)]
    public string LastName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar (10)")]
    [StringLength(10)]
    public string FirstName { get; set; } = null!;

    [Column(TypeName = "nvarchar (30)")]
    [StringLength(30)]
    public string? Title { get; set; }

    [Column(TypeName = "nvarchar (25)")]
    [StringLength(25)]
    public string? TitleOfCourtesy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BirthDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? HireDate { get; set; }

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
    public string? HomePhone { get; set; }

    [Column(TypeName = "nvarchar (4)")]
    [StringLength(4)]
    public string? Extension { get; set; }

    [Column(TypeName = "image")]
    public byte[]? Photo { get; set; }

    [Column(TypeName = "ntext")]
    public string? Notes { get; set; }

    [Column(TypeName = "int")]
    public int? ReportsTo { get; set; }

    [Column(TypeName = "nvarchar (255)")]
    [StringLength(255)]
    public string? PhotoPath { get; set; }

    [InverseProperty(nameof(Order.Employee))]
    public virtual ICollection<Order> Orders { get; set; }
  }
}

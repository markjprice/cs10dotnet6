using Microsoft.AspNetCore.Mvc.RazorPages; // PageModel
using System.Collections.Generic; // IEnumerable<T>
using Packt.Shared; // NorthwindContext
using Microsoft.AspNetCore.Mvc; // [BindProperty], IActionResult
using System.Linq; // OrderBy, ThenBy extension methods

namespace Northwind.Web.Pages
{
  public class SuppliersModel : PageModel
  {
    public IEnumerable<Supplier> Suppliers { get; set; }

    private NorthwindContext db;

    public SuppliersModel(NorthwindContext injectedContext)
    {
      db = injectedContext;
    }
    
    public void OnGet()
    {
      ViewData["Title"] = "Northwind B2B - Suppliers";

      Suppliers = db.Suppliers
        .OrderBy(c => c.Country).ThenBy(c => c.CompanyName);
    }
    
    [BindProperty]
    public Supplier Supplier { get; set; }

    public IActionResult OnPost()
    {
      if (ModelState.IsValid)
      {
        db.Suppliers.Add(Supplier);
        db.SaveChanges();
        return RedirectToPage("/suppliers");
      }
      else
      {
        return Page(); // return to original page
      }
    }
  }
}

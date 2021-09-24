namespace Northwind.Mvc.Models;

public record HomeModelBindingViewModel
(
  Thing Thing, 
  bool HasErrors,
  IEnumerable<string> ValidationErrors
);
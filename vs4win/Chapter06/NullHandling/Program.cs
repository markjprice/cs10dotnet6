using static System.Console;

int thisCannotBeNull = 4;
//thisCannotBeNull = null; // compile error!

int? thisCouldBeNull = null;
WriteLine(thisCouldBeNull);
WriteLine(thisCouldBeNull.GetValueOrDefault());

thisCouldBeNull = 7;
WriteLine(thisCouldBeNull);
WriteLine(thisCouldBeNull.GetValueOrDefault());

// Declaring non-nullable variables and parameters

// Address class is defined above Program class
Address address = new();
address.Building = null;
address.Street = null;
address.City = "London";
address.Region = null;

// Checking for null

string authorName = null;

// the following throws a NullReferenceException 
// int x = authorName.Length;

// instead of throwing an exception, null is assigned to y 
int? y = authorName?.Length;

WriteLine($"y is null: {y is null}");

// result will be 3 if authorName?.Length is null 
int result = authorName?.Length ?? 3;
WriteLine(result);

class Address
{
  public string? Building;
  public string Street = string.Empty;
  public string City = string.Empty;
  public string Region = string.Empty;
}

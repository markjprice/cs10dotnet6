using Packt.Shared;
using static System.Console;

// Setting and outputting field values

// var bob = new Person(); // C# 1.0 or later
Person bob = new();
// WriteLine(bob.ToString());

bob.Name = "Bob Smith";
bob.DateOfBirth = new DateTime(1965, 12, 22); // C# 1.0 or later

WriteLine(format: "{0} was born on {1:dddd, d MMMM yyyy}",
  arg0: bob.Name,
  arg1: bob.DateOfBirth);

Person alice = new()
{
  Name = "Alice Jones",
  DateOfBirth = new(1998, 3, 7) // C# 9.0 or later
};

WriteLine(format: "{0} was born on {1:dd MMM yy}",
  arg0: alice.Name,
  arg1: alice.DateOfBirth);

// Storing a value using an enum type

bob.FavoriteAncientWonder = WondersOfTheAncientWorld.StatueOfZeusAtOlympia;

WriteLine(
  format: "{0}'s favorite wonder is {1}. Its integer is {2}.",
  arg0: bob.Name,
  arg1: bob.FavoriteAncientWonder,
  arg2: (int)bob.FavoriteAncientWonder);

// Storing multiple values using an enum type

bob.BucketList =
  WondersOfTheAncientWorld.HangingGardensOfBabylon
  | WondersOfTheAncientWorld.MausoleumAtHalicarnassus;

// bob.BucketList = (WondersOfTheAncientWorld)18; 

WriteLine($"{bob.Name}'s bucket list is {bob.BucketList}");

// Storing multiple values using collections

bob.Children.Add(new Person { Name = "Alfred" }); // C# 3.0 and later
bob.Children.Add(new() { Name = "Zoe" }); // C# 9.0 and later

WriteLine(
  $"{bob.Name} has {bob.Children.Count} children:");

for (int childIndex = 0; childIndex < bob.Children.Count; childIndex++)
{
  WriteLine($"  {bob.Children[childIndex].Name}");
}

// Making a field static

BankAccount.InterestRate = 0.012M; // store a shared value

BankAccount jonesAccount = new(); // C# 9.0 and later
jonesAccount.AccountName = "Mrs. Jones";
jonesAccount.Balance = 2400;

WriteLine(format: "{0} earned {1:C} interest.",
  arg0: jonesAccount.AccountName,
  arg1: jonesAccount.Balance * BankAccount.InterestRate);

BankAccount gerrierAccount = new();
gerrierAccount.AccountName = "Ms. Gerrier";
gerrierAccount.Balance = 98;

WriteLine(format: "{0} earned {1:C} interest.",
  arg0: gerrierAccount.AccountName,
  arg1: gerrierAccount.Balance * BankAccount.InterestRate);

// Making a field constant

WriteLine($"{bob.Name} is a {Person.Species}");

// Making a field read-only

WriteLine($"{bob.Name} was born on {bob.HomePlanet}");

// Initializing fields with constructors

Person blankPerson = new();

WriteLine(format:
  "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
  arg0: blankPerson.Name,
  arg1: blankPerson.HomePlanet,
  arg2: blankPerson.Instantiated);

Person gunny = new(initialName: "Gunny", homePlanet: "Mars");

WriteLine(format:
  "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
  arg0: gunny.Name,
  arg1: gunny.HomePlanet,
  arg2: gunny.Instantiated);

// Returning values from methods

bob.WriteToConsole();
WriteLine(bob.GetOrigin());

// Combining multiple returned values using tuples

(string, int) fruit = bob.GetFruit();
WriteLine($"{fruit.Item1}, {fruit.Item2} there are.");

// Naming the fields of a tuple

var fruitNamed = bob.GetNamedFruit();
WriteLine($"There are {fruitNamed.Number} {fruitNamed.Name}.");

// Inferring tuple names

var thing1 = ("Neville", 4);
WriteLine($"{thing1.Item1} has {thing1.Item2} children.");

var thing2 = (bob.Name, bob.Children.Count);
WriteLine($"{thing2.Name} has {thing2.Count} children.");

// Deconstructing tuples

(string fruitName, int fruitNumber) = bob.GetFruit();
WriteLine($"Deconstructed: {fruitName}, {fruitNumber}");

// Deconstructing a Person

var (name1, dob1) = bob;
WriteLine($"Deconstructed: {name1}, {dob1}");

var (name2, dob2, fav2) = bob;
WriteLine($"Deconstructed: {name2}, {dob2}, {fav2}");

// Defining and passing parameters to methods

WriteLine(bob.SayHello());
WriteLine(bob.SayHello("Emily"));

// Passing optional parameters and naming arguments

WriteLine(bob.OptionalParameters());

WriteLine(bob.OptionalParameters("Jump!", 98.5));

WriteLine(bob.OptionalParameters(
  number: 52.7, command: "Hide!"));

WriteLine(bob.OptionalParameters("Poke!", active: false));

// Controlling how parameters are passed

int a = 10;
int b = 20;
int c = 30;

WriteLine($"Before: a = {a}, b = {b}, c = {c}");

bob.PassingParameters(a, ref b, out c);

WriteLine($"After: a = {a}, b = {b}, c = {c}");

int d = 10;
int e = 20;

WriteLine(
  $"Before: d = {d}, e = {e}, f doesn't exist yet!");

// simplified C# 7 syntax for the out parameter
bob.PassingParameters(d, ref e, out int f);

WriteLine($"After: d = {d}, e = {e}, f = {f}");

// Defining read-only properties

Person sam = new()
{
  Name = "Sam",
  DateOfBirth = new(1972, 1, 27)
};

WriteLine(sam.Origin);
WriteLine(sam.Greeting);
WriteLine(sam.Age);

// Defining settable properties

sam.FavoriteIceCream = "Chocolate Fudge";

WriteLine($"Sam's favorite ice-cream flavor is {sam.FavoriteIceCream}.");

sam.FavoritePrimaryColor = "Red";

WriteLine($"Sam's favorite primary color is {sam.FavoritePrimaryColor}.");

// Defining indexers

sam.Children.Add(new() { Name = "Charlie" });
sam.Children.Add(new() { Name = "Ella" });

WriteLine($"Sam's first child is {sam.Children[0].Name}");
WriteLine($"Sam's second child is {sam.Children[1].Name}");
WriteLine($"Sam's first child is {sam[0].Name}");
WriteLine($"Sam's second child is {sam[1].Name}");

// Exploring pattern matching

object[] passengers = {
        new FirstClassPassenger { AirMiles = 1_419 },
        new FirstClassPassenger { AirMiles = 16_562 },
        new BusinessClassPassenger(),
        new CoachClassPassenger { CarryOnKG = 25.7 },
        new CoachClassPassenger { CarryOnKG = 0 },
      };

foreach (object passenger in passengers)
{
  decimal flightCost = passenger switch
  {

    /* C# 8 syntax
    FirstClassPassenger p when p.AirMiles > 35000 => 1500M,
    FirstClassPassenger p when p.AirMiles > 15000 => 1750M,
    FirstClassPassenger                           => 2000M, */

    // C# 9 syntax
    FirstClassPassenger p => p.AirMiles switch
    {
      > 35000 => 1500M,
      > 15000 => 1750M,
      _ => 2000M
    },

    BusinessClassPassenger => 1000M,
    CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
    CoachClassPassenger => 650M,
    _ => 800M
  };

  WriteLine($"Flight costs {flightCost:C} for {passenger}");
}

// Working with records

ImmutablePerson jeff = new()
{
  FirstName = "Jeff",
  LastName = "Winger"
};

// the following is not allowed with init properties
// jeff.FirstName = "Geoff";

ImmutableVehicle car = new()
{
  Brand = "Mazda MX-5",
  Color = "Metallic Soul Red",
  Wheels = 4
};

ImmutableVehicle repaintedCar = car
  with { Color = "Polymetallic Grey" };

WriteLine($"Original car color was {car.Color}.");
WriteLine($"New car color is {repaintedCar.Color}.");

ImmutableAnimal oscar = new("Oscar", "Labrador");
var (who, what) = oscar; // calls Deconstruct method
WriteLine($"{who} is a {what}.");
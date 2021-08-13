using static System.Console;

try
{
  checked
  {
    int x = int.MaxValue - 1;
    WriteLine($"Initial value: {x}");
    x++;
    WriteLine($"After incrementing: {x}");
    x++;
    WriteLine($"After incrementing: {x}");
    x++;
    WriteLine($"After incrementing: {x}");
  }
}
catch (OverflowException)
{
  WriteLine("The code overflowed but I caught the exception.");
}

unchecked
{
  int y = int.MaxValue + 1;

  WriteLine($"Initial value: {y}");
  y--;
  WriteLine($"After decrementing: {y}");
  y--;
  WriteLine($"After decrementing: {y}");
}

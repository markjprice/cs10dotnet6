using Packt.Shared; // ToWords extension method
using System.Numerics; // BigInteger

using static System.Console;

Write("Enter a number up to twenty one digits long: ");
string? input = ReadLine();
if (input is null) return;

if (input.Length > 21)
{
  WriteLine("I cannot handle more than twenty one digits!");
  return;
}

BigInteger number = BigInteger.Parse(input);

WriteLine($"{number:N0} in words is {number.ToWords()}.");

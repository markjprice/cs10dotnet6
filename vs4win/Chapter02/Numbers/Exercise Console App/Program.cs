uint naturalNumber = 2000; //Positive integer from 0 and above
int integerNumber = 20; //Negative and positive whole number
float realNumber = 2.3F; //single-precision floating point, F suffix allows compiler to interpret the number as floating point rather than integer
double largerRealNumber = 2.435345; //larger-bit-size floating point
// Interpreted as a double literal, no need to include a suffix

int decimalNotation = 2_000_000;//automatically formats integers to have underscore
//Could create a config file that IDE could automatically format all decimalnumbers with underscore
double realNumberNotation = 2_123_342.23453; //Double with underscore notation
int binaryNotation = 0b_0001_1110_1000_0100_1000_0000;
//Integer has 32 bits, therefore 4 bytes.  Not necessary but worth looking into

int hexadecimalNotation = 0x_001E_8480;
// check the three variables have the same value
// both statements output true 
Console.WriteLine($"{decimalNotation == binaryNotation}");
Console.WriteLine($"{decimalNotation == hexadecimalNotation}");

Console.WriteLine($"int uses {sizeof(int)} bytes and can store numbers in the range {int.MinValue:N0} to {int.MaxValue:N0}.");
Console.WriteLine($"double uses {sizeof(double)} bytes and can store numbers in the range {double.MinValue:N0} to {double.MaxValue:N0}.");
Console.WriteLine($"decimal uses {sizeof(decimal)} bytes and can store numbers in the range {decimal.MinValue:N0} to {decimal.MaxValue:N0}.");

Console.WriteLine("Using decimals data type:");
decimal c = 0.1M; // M suffix means a decimal literal value
decimal d = 0.2M;
if (c + d == 0.3M)
{
    Console.WriteLine($"{c} + {d} equals {0.3M}");
}
else
{
    Console.WriteLine($"{c} + {d} does NOT equal {0.3M}");
}
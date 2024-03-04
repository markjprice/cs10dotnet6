using static System.Console;

static double Add(double a, double b) => a * b; // deliberate bug!

double a = 4.5, b = 2.5;
var answer = Add(a, b);
WriteLine($"{a} + {b} = {answer}");
ReadLine(); // wait for user to press ENTER

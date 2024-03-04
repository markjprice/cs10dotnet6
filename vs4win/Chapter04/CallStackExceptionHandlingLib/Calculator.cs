using static System.Console;

namespace CallStackExceptionHandlingLib;

public static class Calculator
{
    public static void Gamma() // public so it can be called from outside
    {
        WriteLine("In Gamma");
        Delta();
    }

    private static void Delta() // private so it can only be called internally
    {
        WriteLine("In Delta");
        _ = File.OpenText("bad file path");
    }
}

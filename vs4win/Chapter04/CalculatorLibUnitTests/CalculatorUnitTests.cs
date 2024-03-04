using CalculatorLib;
using Xunit;

namespace CalculatorLibUnitTests;

public class CalculatorUnitTests
{
    [Fact]
    public void TestAdding2And2()
    {
        // arrange 
        double a = 2, b = 2, expected = 4;
        Calculator calc = new();

        // act
        var actual = calc.Add(a, b);

        // assert 
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestAdding2And3()
    {
        // arrange 
        double a = 2, b = 3, expected = 5;
        Calculator calc = new();

        // act
        var actual = calc.Add(a, b);

        // assert 
        Assert.Equal(expected, actual);
    }
}
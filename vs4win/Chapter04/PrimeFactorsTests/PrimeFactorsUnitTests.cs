using PrimeFactorsLib;
using Xunit;

namespace PrimeFactorsTests;

public class PrimeFactorsTests
{
    [Fact]
    public void PrimeFactorsOf40()
    {
        // arrange
        var number = 40;
        var expected = "5 x 2 x 2 x 2";

        // act
        var actual = Primes.PrimeFactors(number);

        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrimeFactorsOf99()
    {
        // arrange
        var number = 99;
        var expected = "11 x 3 x 3";

        // act
        var actual = Primes.PrimeFactors(number);

        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrimeFactorsOf519()
    {
        // arrange
        var number = 519;
        var expected = "173 x 3";

        // act
        var actual = Primes.PrimeFactors(number);

        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrimeFactorsOf997()
    {
        // arrange
        var number = 997;
        var expected = "997";

        // act
        var actual = Primes.PrimeFactors(number);

        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrimeFactorsOf1000()
    {
        // arrange
        var number = 1000;
        var expected = "5 x 5 x 5 x 2 x 2 x 2";

        // act
        var actual = Primes.PrimeFactors(number);

        // assert
        Assert.Equal(expected, actual);
    }
}
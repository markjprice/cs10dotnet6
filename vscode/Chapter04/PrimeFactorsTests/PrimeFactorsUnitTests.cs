using Xunit;

namespace PrimeFactors
{
  public class PrimeFactorsTests
  {
    [Fact]
    public void PrimeFactorsOf40()
    {
      // arrange
      int number = 40;
      string expected = "5 x 2 x 2 x 2";

      // act
      string actual = Primes.PrimeFactors(number);

      // assert
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrimeFactorsOf99()
    {
      // arrange
      int number = 99;
      string expected = "11 x 3 x 3";

      // act
      string actual = Primes.PrimeFactors(number);

      // assert
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrimeFactorsOf519()
    {
      // arrange
      int number = 519;
      string expected = "173 x 3";

      // act
      string actual = Primes.PrimeFactors(number);

      // assert
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrimeFactorsOf997()
    {
      // arrange
      int number = 997;
      string expected = "997";

      // act
      string actual = Primes.PrimeFactors(number);

      // assert
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrimeFactorsOf1000()
    {
      // arrange
      int number = 1000;
      string expected = "5 x 5 x 5 x 2 x 2 x 2";

      // act
      string actual = Primes.PrimeFactors(number);

      // assert
      Assert.Equal(expected, actual);
    }
  }
}
using static System.Console;

Doer.DoSomething();

public interface IWithStaticAbstract
{
  static abstract void DoSomething();
}

public class Doer : IWithStaticAbstract
{
  public static void DoSomething()
  {
    WriteLine("I am an implementation of a static abstract method.");
  }
}

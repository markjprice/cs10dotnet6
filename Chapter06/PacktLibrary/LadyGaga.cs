using static System.Console;

namespace Packt.Shared
{
  public class Singer
  {
    public virtual void Sing()
    {
      WriteLine("Singing...");
    }
  }

  public class LadyGaga : Singer
  {
    public sealed override void Sing()
    {
      WriteLine("Singing with style...");
    }
  }
}
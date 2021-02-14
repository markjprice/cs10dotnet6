using static System.Console;

namespace Packt.Shared
{
  public class DvdPlayer : IPlayable
  {
    public void Pause()
    {
      WriteLine("DVD player is pausing.");
    }

    public void Play()
    {
      WriteLine("DVD player is playing.");
    }
  }
}
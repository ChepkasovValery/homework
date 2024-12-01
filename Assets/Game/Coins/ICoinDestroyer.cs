using Modules;

namespace Game.Coins
{
  public interface ICoinDestroyer
  {
    void Destroy(ICoin coin);
  }
}
using System;

namespace Game.Coins
{
  public interface ICoinManager
  {
    event Action OnAllCoinsCollected;
    void SpawnCoins(int count);
  }
}
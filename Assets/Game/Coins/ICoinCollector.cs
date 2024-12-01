using System;
using Modules;

namespace Game.Coins
{
  public interface ICoinCollector
  {
    event Action<ICoin> OnCoinCollected;
    event Action OnAllCoinsCollected;
  }
}
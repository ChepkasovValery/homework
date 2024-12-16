using System;
using System.Collections.Generic;
using Modules;

namespace Snake
{
  public interface ISnakeCoinCollector
  {
    event Action<ICoin> OnCoinCollected;
    void SetCoins(IReadOnlyList<ICoin> coins);
  }
}
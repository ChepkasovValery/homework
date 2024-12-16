using System;
using Modules;
using UnityEngine;
using Zenject;

namespace Game.Coins
{
  public class CoinPool : MonoMemoryPool<Vector2Int, Coin>
  {
    protected override void Reinitialize(Vector2Int position, Coin coin)
    {
      coin.Position = position;
      coin.Generate();
    }
  }
}
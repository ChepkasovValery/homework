using System;
using Modules;
using UnityEngine;
using Zenject;

namespace Game.Coins
{
  public class CoinPool : MonoMemoryPool<Vector2Int, Coin>, ICoinSpawner, ICoinDestroyer
  {
    public event Action<ICoin> OnCoinSpawned;

    ICoin ICoinSpawner.Spawn(Vector2Int position)
    {
      return Spawn(position);
    }

    public void Destroy(ICoin coin)
    {
      Despawn(coin as Coin);
    }

    protected override void Reinitialize(Vector2Int position, Coin coin)
    {
      coin.Position = position;
      coin.Generate();
      
      OnCoinSpawned?.Invoke(coin);
    }
  }
}
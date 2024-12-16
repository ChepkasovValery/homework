using System;
using System.Collections.Generic;
using Modules;
using Snake;
using SnakeGame;
using Zenject;

namespace Game.Coins
{
  public class CoinManager : IInitializable, IDisposable, ICoinManager
  {
    public event Action OnAllCoinsCollected;
      
    private readonly IWorldBounds _worldBounds;
    private readonly CoinPool _coinPool;
    private readonly ISnakeCoinCollector _snakeCoinCollector;
    
    private List<ICoin> _coins;

    public CoinManager(IWorldBounds worldBounds, CoinPool coinPool, ISnakeCoinCollector snakeCoinCollector)
    {
      _snakeCoinCollector = snakeCoinCollector;
      _coinPool = coinPool;
      _worldBounds = worldBounds;
    }

    public void Initialize()
    {
      _snakeCoinCollector.OnCoinCollected += RemoveCoin;
    }

    public void Dispose()
    {
      _snakeCoinCollector.OnCoinCollected -= RemoveCoin;
    }

    public void SpawnCoins(int count)
    {
      _coins = new List<ICoin>(count);
      
      for (int i = 0; i < count; i++)
      {
        ICoin coin = _coinPool.Spawn(_worldBounds.GetRandomPosition());
        
        _coins.Add(coin);
      }
      
      _snakeCoinCollector.SetCoins(_coins);
    }

    private void RemoveCoin(ICoin coin)
    {
      _coinPool.Despawn((Coin) coin);
      _coins.Remove(coin);
      
      if (_coins.Count == 0)
      {
        OnAllCoinsCollected?.Invoke();
      }
    }
  }
}
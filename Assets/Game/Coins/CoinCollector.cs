using System;
using System.Collections.Generic;
using Modules;
using UnityEngine;
using Zenject;

namespace Game.Coins
{
  public class CoinCollector : IInitializable, IDisposable, ICoinCollector
  {
    public event Action<ICoin> OnCoinCollected;
    public event Action OnAllCoinsCollected;

    private readonly ISnake _snake;
    private readonly ICoinSpawner _coinSpawner;
    private readonly ICoinDestroyer _coinDestroyer;
    
    private List<ICoin> _coins;

    public CoinCollector(ISnake snake, ICoinSpawner coinSpawner, ICoinDestroyer coinDestroyer)
    {
      _coinDestroyer = coinDestroyer;
      _snake = snake;
      _coinSpawner = coinSpawner;
    }
      
    public void Initialize()
    {
      _coinSpawner.OnCoinSpawned += AddCoinToObserve;
      _snake.OnMoved += SnakeMoved;
      
      _coins = new List<ICoin>();
    }

    public void Dispose()
    {
      _coinSpawner.OnCoinSpawned -= AddCoinToObserve;
      _snake.OnMoved -= SnakeMoved;
    }

    private void SnakeMoved(Vector2Int pos)
    {
      foreach (ICoin coin in _coins)
      {
        if (pos == coin.Position)
        {
          _coinDestroyer.Destroy(coin);
          _coins.Remove(coin);
          
          OnCoinCollected?.Invoke(coin);
          
          if (_coins.Count == 0)
          {
            OnAllCoinsCollected?.Invoke();
          }
          
          break;
        }
      }
    }

    private void AddCoinToObserve(ICoin coin)
    {
      _coins.Add(coin);
    }
  }
}
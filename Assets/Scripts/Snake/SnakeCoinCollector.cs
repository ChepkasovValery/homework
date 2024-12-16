using System;
using System.Collections.Generic;
using Game.Coins;
using Modules;
using UnityEngine;
using Zenject;

namespace Snake
{
  public class SnakeCoinCollector : IInitializable, IDisposable, ISnakeCoinCollector
  {
    public event Action<ICoin> OnCoinCollected;

    private readonly ISnake _snake;

    private IReadOnlyList<ICoin> _coins;

    public SnakeCoinCollector(ISnake snake)
    {
      _snake = snake;
    }

    public void Initialize()
    {
      _snake.OnMoved += SnakeMoved;
    }

    public void Dispose()
    {
      _snake.OnMoved -= SnakeMoved;
    }

    public void SetCoins(IReadOnlyList<ICoin> coins)
    {
      _coins = coins;
    }

    private void SnakeMoved(Vector2Int pos)
    {
      foreach (ICoin coin in _coins)
      {
        if (pos == coin.Position)
        {
          OnCoinCollected?.Invoke(coin);
          
          break;
        }
      }
    }
  }
}
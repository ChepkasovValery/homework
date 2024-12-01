using System;
using Game.Coins;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Game.GameState
{
  public class GameOverObserver: IGameOverObserver, IInitializable, IDisposable
  {
    public event Action OnWin;
    public event Action OnLoose;
    
    private readonly ISnake _snake;
    private readonly IWorldBounds _worldBounds;
    private readonly IDifficulty _difficulty;
    private readonly ICoinCollector _coinCollector;

    public GameOverObserver(ISnake snake, IWorldBounds worldBounds,
      IDifficulty difficulty, ICoinCollector coinCollector)
    {
      _coinCollector = coinCollector;
      _snake = snake;
      _worldBounds = worldBounds;
      _difficulty = difficulty;
    }

    public void Initialize()
    {
      _snake.OnSelfCollided += GameOver;
      _snake.OnMoved += CheckSnakeOutOfBounds;
      _coinCollector.OnAllCoinsCollected += CheckWin;
    }

    public void Dispose()
    {
      _snake.OnSelfCollided -= GameOver;
      _snake.OnMoved -= CheckSnakeOutOfBounds;
      _coinCollector.OnAllCoinsCollected -= CheckWin;
    }

    private void GameOver()
    {
      OnLoose?.Invoke();
    }

    private void CheckWin()
    {
      if (_difficulty.Current == _difficulty.Max)
      {
        OnWin?.Invoke();
      }
    }

    private void CheckSnakeOutOfBounds(Vector2Int pos)
    {
      if (!_worldBounds.IsInBounds(pos))
      {
        GameOver();
      }
    }
  }
}
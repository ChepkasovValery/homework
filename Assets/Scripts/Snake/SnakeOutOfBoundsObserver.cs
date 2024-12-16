using System;
using GameState;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Snake
{
  public class SnakeOutOfBoundsObserver : IInitializable, IDisposable
  {
    private readonly ISnake _snake;
    private readonly IWorldBounds _worldBounds;
    private readonly IGameCycle _gameCycle;

    public SnakeOutOfBoundsObserver(ISnake snake, IWorldBounds worldBounds, IGameCycle gameCycle)
    {
      _gameCycle = gameCycle;
      _snake = snake;
      _worldBounds = worldBounds;
    }

    public void Initialize()
    {
      _snake.OnMoved += CheckSnakeOutOfBounds;
    }

    public void Dispose()
    {
      _snake.OnMoved -= CheckSnakeOutOfBounds;
    }
    
    private void CheckSnakeOutOfBounds(Vector2Int pos)
    {
      if (!_worldBounds.IsInBounds(pos))
      {
        _gameCycle.Lose();
      }
    }
  }
}
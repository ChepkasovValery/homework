using System;
using GameState;
using Modules;
using Zenject;

namespace Snake
{
  public class SnakeSelfCollidedObserver : IInitializable, IDisposable
  {
    private readonly ISnake _snake;
    private readonly IGameCycle _gameCycle;

    public SnakeSelfCollidedObserver(ISnake snake, IGameCycle gameCycle)
    {
      _gameCycle = gameCycle;
      _snake = snake;
    }

    public void Initialize()
    {
      _snake.OnSelfCollided += _gameCycle.Lose;
    }

    public void Dispose()
    {
      _snake.OnSelfCollided -= _gameCycle.Lose;
    }
  }
}
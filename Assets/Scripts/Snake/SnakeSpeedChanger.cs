using System;
using Modules;
using Zenject;

namespace Snake
{
  public class SnakeSpeedChanger : IInitializable, IDisposable
  {
    private readonly IDifficulty _difficulty;
    private readonly ISnake _snake;

    public SnakeSpeedChanger(IDifficulty difficulty, ISnake snake)
    {
      _snake = snake;
      _difficulty = difficulty;
    }

    public void Initialize()
    {
      _difficulty.OnStateChanged += IncreaseSpeed;
    }

    public void Dispose()
    {
      _difficulty.OnStateChanged -= IncreaseSpeed;
    }

    private void IncreaseSpeed()
    {
      _snake.SetSpeed(_difficulty.Current);
    }
  }
}
using System;
using Modules;
using Zenject;

namespace Game
{
  public class SnakeMover : IInitializable, IDisposable
  {
    private readonly IInput _input;
    private readonly ISnake _snake;

    public SnakeMover(IInput input, ISnake snake)
    {
      _snake = snake;
      _input = input;
    }

    public void Initialize()
    {
      _input.OnInputDirection += _snake.Turn;
    }

    public void Dispose()
    {
      _input.OnInputDirection -= _snake.Turn;
    }
  }
}

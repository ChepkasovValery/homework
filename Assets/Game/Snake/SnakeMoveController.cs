using System;
using Modules;
using UnityEngine;
using Zenject;

namespace Game
{
  public class SnakeMoveController : IInitializable, IDisposable
  {
    private readonly IInput _input;
    private readonly ISnake _snake;

    public SnakeMoveController(IInput input, ISnake snake)
    {
      _snake = snake;
      _input = input;
    }

    public void Initialize()
    {
      _input.OnInputDirection += TurnSnake;
    }

    public void Dispose()
    {
      _input.OnInputDirection -= TurnSnake;
    }

    private void TurnSnake(Vector2Int direction)
    {
      _snake.Turn(ToSnakeDirection(direction));
    }

    private SnakeDirection ToSnakeDirection(Vector2Int direction)
    {
      if (direction == Vector2Int.left)
        return SnakeDirection.LEFT;
      else if (direction == Vector2Int.right)
        return SnakeDirection.RIGHT;
      else if (direction == Vector2Int.up)
        return SnakeDirection.UP;
      else if (direction == Vector2Int.down)
        return SnakeDirection.DOWN;

      return SnakeDirection.NONE;
    }
  }
}

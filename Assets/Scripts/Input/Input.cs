using System;
using UnityEngine;
using Zenject;

namespace Modules
{
  public class Input : IInput, ITickable
  {
    public event Action<SnakeDirection> OnInputDirection;
      
    public void Tick()
    {
      if (UnityEngine.Input.GetKeyDown(KeyCode.A) ||
          UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
      {
        OnInputDirection?.Invoke(ToSnakeDirection(Vector2Int.left));
      }
      
      if (UnityEngine.Input.GetKeyDown(KeyCode.D) ||
          UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
      {
        OnInputDirection?.Invoke(ToSnakeDirection(Vector2Int.right));
      }
      
      if (UnityEngine.Input.GetKeyDown(KeyCode.W) ||
          UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
      {
        OnInputDirection?.Invoke(ToSnakeDirection(Vector2Int.up));
      }
      
      if (UnityEngine.Input.GetKeyDown(KeyCode.S) ||
          UnityEngine.Input.GetKeyDown(KeyCode.DownArrow))
      {
        OnInputDirection?.Invoke(ToSnakeDirection(Vector2Int.down));
      }
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
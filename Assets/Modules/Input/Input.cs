using System;
using UnityEngine;
using Zenject;

namespace Modules
{
  public class Input : IInput, ITickable
  {
    public event Action<Vector2Int> OnInputDirection;
      
    public void Tick()
    {
      if (UnityEngine.Input.GetKeyDown(KeyCode.A) ||
          UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
      {
        OnInputDirection?.Invoke(Vector2Int.left);
      }
      
      if (UnityEngine.Input.GetKeyDown(KeyCode.D) ||
          UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
      {
        OnInputDirection?.Invoke(Vector2Int.right);
      }
      
      if (UnityEngine.Input.GetKeyDown(KeyCode.W) ||
          UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
      {
        OnInputDirection?.Invoke(Vector2Int.up);
      }
      
      if (UnityEngine.Input.GetKeyDown(KeyCode.S) ||
          UnityEngine.Input.GetKeyDown(KeyCode.DownArrow))
      {
        OnInputDirection?.Invoke(Vector2Int.down);
      }
    }
  }
}
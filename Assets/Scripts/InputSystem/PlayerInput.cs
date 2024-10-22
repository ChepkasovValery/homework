using System;
using UnityEngine;

namespace Game.InputSystem
{
  public class PlayerInput : MonoBehaviour
  {
    public event Action OnFirePressed;

    public Vector2 MoveDirection => _currentInput;

    private Vector2 _currentInput;
    
    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        OnFirePressed?.Invoke();
      }

      if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
      {
        _currentInput.x = -1;
      }
      else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
      {
        _currentInput.x = 1;
      }
      else
      {
        _currentInput.x = 0;
      }
    }
  }
}
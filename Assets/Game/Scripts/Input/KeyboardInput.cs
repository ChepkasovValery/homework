using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Input
{
  public class KeyboardInput : IInput, ITickable
  {
    public event Action OnJumpPressed;
    public Vector2 MoveDirection => _direction;

    private Vector2 _direction;
    
    public void Tick()
    {
      _direction.x = UnityEngine.Input.GetAxis("Horizontal");

      if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
      {
        OnJumpPressed?.Invoke();
      }
    }
  }
}
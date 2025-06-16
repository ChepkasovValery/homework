using System;
using UnityEngine;
using Zenject;

namespace Game.Modules.Input.Scripts
{
  public class KeyboardInput : IInput, ITickable
  {
    public event Action OnJumpPressed;
    public event Action OnTossAttackPressed;
    public event Action OnPushAttackPressed;
    public Vector2 MoveDirection => _direction;

    private Vector2 _direction;
    
    public void Tick()
    {
      _direction.x = UnityEngine.Input.GetAxis("Horizontal");

      if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
      {
        OnJumpPressed?.Invoke();
      }

      if (UnityEngine.Input.GetMouseButtonDown(0))
      {
        OnTossAttackPressed?.Invoke();
      }
      
      if (UnityEngine.Input.GetMouseButtonDown(1))
      {
        OnPushAttackPressed?.Invoke();
      }
    }
  }
}
using System;
using UnityEngine;

namespace Game.Modules.Input.Scripts
{
  public interface IInput
  {
    Vector2 MoveDirection { get; }
    event Action OnJumpPressed;
    event Action OnTossAttackPressed;
    event Action OnPushAttackPressed;
  }
}

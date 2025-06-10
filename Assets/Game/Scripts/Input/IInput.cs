using System;
using UnityEngine;

namespace Game.Scripts.Input
{
  public interface IInput
  {
    Vector2 MoveDirection { get; }
    event Action OnJumpPressed;
  }
}

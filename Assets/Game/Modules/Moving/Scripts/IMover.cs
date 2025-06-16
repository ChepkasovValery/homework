using System;
using UnityEngine;

namespace Game.Modules.Moving.Scripts
{
  public interface IMover
  {
    Vector3 Position { get; }
    void Move(Vector2 velocity);
  }
}
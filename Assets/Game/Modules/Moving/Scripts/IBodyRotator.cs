using UnityEngine;

namespace Game.Modules.Moving.Scripts
{
  public interface IBodyRotator
  {
    void LookToDirection(Vector2 direction);
  }
}
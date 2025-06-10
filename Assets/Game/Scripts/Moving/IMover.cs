using UnityEngine;

namespace Game.Scripts.Moving
{
  public interface IMover
  {
    void Move(Vector2 velocity);
    void Jump();
  }
}
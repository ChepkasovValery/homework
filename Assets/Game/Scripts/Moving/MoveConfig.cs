using UnityEngine;

namespace Game.Scripts.Moving
{
  [CreateAssetMenu(menuName = "Configs/Moving Config", fileName = "Move config")]
  public class MoveConfig : ScriptableObject
  {
    public float Speed;
    public float JumpForce;
  }
}
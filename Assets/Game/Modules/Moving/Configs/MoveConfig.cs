using UnityEngine;

namespace Game.Modules.Moving.Configs
{
  [CreateAssetMenu(menuName = "Configs/Moving/Move Config", fileName = "Move config")]
  public class MoveConfig : ScriptableObject
  {
    public float Speed;
    public float Acceleration;
  }
}
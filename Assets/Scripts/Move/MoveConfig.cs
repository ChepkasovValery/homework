using UnityEngine;

namespace Game.Configs
{
  [CreateAssetMenu(menuName = "Configs/Move config", fileName = "move config")]
  public class MoveConfig : ScriptableObject
  {
    public float Speed;
  }
}
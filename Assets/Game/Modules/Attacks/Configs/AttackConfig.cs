using UnityEngine;

namespace Game.Modules.Attacks.Configs
{
  [CreateAssetMenu(menuName = "Configs/Attacks/Attack", fileName = "Attack config")]
  public class AttackConfig : ScriptableObject
  {
    public float Force;
  }
}
using UnityEngine;

namespace Game.Modules.Damage.Scripts
{
  [CreateAssetMenu(menuName = "Configs/Damage/Damage config", fileName = "Damage config")]
  public class DamageConfig : ScriptableObject
  {
    public float Damage;
  }
}
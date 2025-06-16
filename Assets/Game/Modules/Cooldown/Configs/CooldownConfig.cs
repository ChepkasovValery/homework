using UnityEngine;

namespace Game.Modules.Cooldown.Configs
{
  [CreateAssetMenu(menuName = "Configs/Cooldown", fileName = "Cooldown config")]
  public class CooldownConfig : ScriptableObject
  {
    public float Cooldown;
  }
}
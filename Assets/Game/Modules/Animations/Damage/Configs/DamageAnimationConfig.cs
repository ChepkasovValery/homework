using UnityEngine;

namespace Game.Modules.Animations.Damage.Configs
{
  [CreateAssetMenu(menuName = "Configs/Animations/Damage", fileName = "Damage animation")]
  public class DamageAnimationConfig : ScriptableObject
  {
    public float DamageDuration;
    public int DamageCycleCount;
    public Color DamageColor;
  }
}
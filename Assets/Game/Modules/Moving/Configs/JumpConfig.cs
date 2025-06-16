using UnityEngine;

namespace Game.Modules.Moving.Configs
{
  [CreateAssetMenu(menuName = "Configs/Moving/Jump Config", fileName = "Jump config")]
  public class JumpConfig : ScriptableObject
  {
    public float Force;
  }
}
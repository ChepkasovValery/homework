using UnityEngine;

namespace Game.Modules.Character.Animations.Configs
{
  [CreateAssetMenu(menuName = "Configs/Character/Animations/Character Animations", fileName = "Character animations")]
  public class CharacterAnimationsConfig : ScriptableObject
  {
    [Header("Jump")]
    public Vector3 JumpScale;
    public float JumpDuration;
  }
}
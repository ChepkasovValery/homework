using UnityEngine;

namespace Game.Modules.SoundSystem.Scripts
{
  [CreateAssetMenu(menuName = "Configs/Sounds", fileName = "Sounds")]
  public class SoundsConfig : ScriptableObject
  {
    public Sounds Sounds;
  }
}
using UnityEngine;

namespace Game.Modules.SoundSystem.Scripts
{
  public interface ISoundManager
  {
    Sounds Sounds { get; }
    void PlayOneShot(AudioClip clip);
    void PlayOneShot(AudioClip clip, Vector3 at);
  }
}
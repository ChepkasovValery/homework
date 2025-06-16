using UnityEngine;

namespace Game.Modules.SoundSystem.Scripts
{
  public class SoundManager : ISoundManager
  {
    public Sounds Sounds => _soundsConfig.Sounds;
    
    private readonly SoundPlayerPool _soundPlayerPool;
    private readonly SoundsConfig _soundsConfig;

    public SoundManager(SoundPlayerPool soundPlayerPool, SoundsConfig soundsConfig)
    {
      _soundPlayerPool = soundPlayerPool;
      _soundsConfig = soundsConfig;
    }

    public void PlayOneShot(AudioClip clip)
    {
      SoundPlayer soundPlayer = _soundPlayerPool.Spawn(Vector3.zero);
      soundPlayer.PlayOneShot(clip);
    }

    public void PlayOneShot(AudioClip clip, Vector3 at)
    {
      SoundPlayer soundPlayer = _soundPlayerPool.Spawn(at);
      soundPlayer.PlayOneShot(clip);
    }
  }
}
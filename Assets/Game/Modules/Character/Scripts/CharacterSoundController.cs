using System;
using Game.Modules.Moving.Scripts;
using Game.Modules.SoundSystem.Scripts;
using Modules.Health.Scripts;
using Zenject;

namespace Game.Modules.Character.Scripts
{
  public class CharacterSoundController : IInitializable, IDisposable
  {
    private readonly ISoundManager _soundManager;
    private readonly IMover _mover;
    private readonly IHealth _health;
    private readonly IJumper _jumper;

    public CharacterSoundController(ISoundManager soundManager, IMover mover, IHealth health, IJumper jumper)
    {
      _soundManager = soundManager;
      _mover = mover;
      _health = health;
      _jumper = jumper;
    }

    public void Initialize()
    {
      _jumper.OnJumped += PlayJumpSound;

      _health.OnDamaged += PlayDamage;
    }

    public void Dispose()
    {
      _jumper.OnJumped -= PlayJumpSound;
      
      _health.OnDamaged -= PlayDamage;
    }

    private void PlayJumpSound()
    {
      _soundManager.PlayOneShot(_soundManager.Sounds.Jump);
    }

    private void PlayDamage(float damage)
    {
      _soundManager.PlayOneShot(_soundManager.Sounds.PlayerDamage);
    }
  }
}
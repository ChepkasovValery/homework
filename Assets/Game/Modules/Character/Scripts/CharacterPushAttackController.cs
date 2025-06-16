using System;
using Game.Modules.Attacks.Api;
using Game.Modules.Attacks.Push.Scripts;
using Game.Modules.Cooldown.Scripts;
using Game.Modules.GroundChecker.Scripts;
using Game.Modules.Input.Scripts;
using Game.Modules.SoundSystem.Scripts;
using Game.Modules.Trigger.Scripts;
using Modules.Health.Scripts;
using Zenject;

namespace Game.Modules.Character.Scripts
{
  public class CharacterPushAttackController : IInitializable, IDisposable
  {
    private readonly IInput _input;
    private readonly ISoundManager _soundManager;
    private readonly ITriggerWatcher _attackTrigger;
    private readonly IGroundChecker _groundChecker;
    private readonly IAttack _pushAttack;
    private readonly ICooldown _cooldown;
    private readonly IHealth _health;

    public CharacterPushAttackController(IInput input, ISoundManager soundManager, PushAttack pushAttack, ITriggerWatcher attackTrigger, 
      IHealth health, ICooldown cooldown, IGroundChecker groundChecker)
    {
      _input = input;
      _soundManager = soundManager;
      _pushAttack = pushAttack;
      _attackTrigger = attackTrigger;
      _health = health;
      _cooldown = cooldown;
      _groundChecker = groundChecker;
    }

    public void Initialize()
    {
      _input.OnPushAttackPressed += TryPushAttack;
    }

    public void Dispose()
    {
      _input.OnPushAttackPressed -= TryPushAttack;
    }

    private void TryPushAttack()
    {
      if(!_health.IsAlive || !_cooldown.IsReady() || !_groundChecker.IsGrounded())
        return;
        
      if (_pushAttack.TryAttack(_attackTrigger.EntitiesInTrigger))
      {
        _soundManager.PlayOneShot(_soundManager.Sounds.Push);
        _cooldown.Reset();
      }
    }
  }
}
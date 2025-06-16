using System;
using Game.Modules.Attacks.Api;
using Game.Modules.Attacks.Toss.Scripts;
using Game.Modules.Cooldown.Scripts;
using Game.Modules.Input.Scripts;
using Game.Modules.SoundSystem.Scripts;
using Game.Modules.Trigger.Scripts;
using Modules.Health.Scripts;
using Zenject;

namespace Game.Modules.Character.Scripts
{
  public class CharacterTossAttackController : IInitializable, IDisposable
  {
    private readonly IInput _input;
    private readonly ISoundManager _soundManager;
    private readonly ITriggerWatcher _attackTrigger;
    private readonly IAttack _attack;
    private readonly ICooldown _cooldown;
    private readonly IHealth _health;

    public CharacterTossAttackController(IInput input, ISoundManager soundManager, TossAttack attack, ITriggerWatcher attackTrigger, 
      IHealth health, ICooldown cooldown)
    {
      _input = input;
      _soundManager = soundManager;
      _attack = attack;
      _attackTrigger = attackTrigger;
      _health = health;
      _cooldown = cooldown;
    }

    public void Initialize()
    {
      _input.OnTossAttackPressed += TryTossAttack;
    }

    public void Dispose()
    {
      _input.OnTossAttackPressed -= TryTossAttack;
    }

    private void TryTossAttack()
    {
      if(!_health.IsAlive || !_cooldown.IsReady())
        return;
        
      if (_attack.TryAttack(_attackTrigger.EntitiesInTrigger))
      {
        _soundManager.PlayOneShot(_soundManager.Sounds.Toss);
        _cooldown.Reset();
      }
    }
  }
}
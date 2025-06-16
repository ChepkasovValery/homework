using System;
using Game.Modules.Attacks.Api;
using Game.Modules.Damage.Scripts;
using Game.Modules.Trigger.Scripts;
using Modules.Common;
using Modules.Health.Scripts;
using Zenject;

namespace Game.Modules.Attacks.Controllers
{
  public class AttackController : IInitializable, IDisposable
  {
    private readonly ITriggerWatcher _triggerWatcher;
    private readonly IAttack _attack;
    private readonly IDamageDealer _damageDealer;

    public AttackController(ITriggerWatcher triggerWatcher, IAttack attack, IDamageDealer damageDealer)
    {
      _triggerWatcher = triggerWatcher;
      _attack = attack;
      _damageDealer = damageDealer;
    }

    public void Initialize()
    {
      _triggerWatcher.OnTriggerEnter += TryAttack;
    }

    public void Dispose()
    {
      _triggerWatcher.OnTriggerExit -= TryAttack;
    }

    private void TryAttack(IEntity target)
    {
      if (target.TryGet(out IHealth health))
      {
        _damageDealer.DealDamage(target);
        _attack.TryAttack(target);
      }
    }
  }
}
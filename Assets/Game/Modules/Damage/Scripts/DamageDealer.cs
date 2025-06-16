using Modules.Common;
using Modules.Health.Scripts;

namespace Game.Modules.Damage.Scripts
{
  public class DamageDealer : IDamageDealer
  {
    private readonly DamageConfig _config;

    public DamageDealer(DamageConfig config)
    {
      _config = config;
    }

    public bool DealDamage(IEntity target)
    {
      if (target.TryGet(out IHealth health))
      {
        health.TakeDamage(_config.Damage);

        return true;
      }

      return false;
    }
  }
}
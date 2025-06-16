using Modules.Common;

namespace Game.Modules.Damage.Scripts
{
  public interface IDamageDealer
  {
    bool DealDamage(IEntity target);
  }
}
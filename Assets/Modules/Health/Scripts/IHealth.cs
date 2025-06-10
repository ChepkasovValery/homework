using System;

namespace Modules.Health.Scripts
{
  public interface IHealth
  {
    event Action OnEnded;
    event Action<float> OnDamaged;
    void Reset();
    void TakeDamage(float damage);
  }
}
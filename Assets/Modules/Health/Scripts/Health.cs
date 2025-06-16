using System;

namespace Modules.Health.Scripts
{
  public class Health : IHealth
  {
    public bool IsAlive => _current > 0;

    public event Action OnEnded;
    public event Action<float> OnDamaged;

    private float _max;
    private float _current;

    public Health(HealthConfig config)
    {
      _max = config.Health;
      _current = config.Health;
    }

    public void Reset()
    {
      _current = _max;
    }

    public void TakeDamage(float damage)
    {
      _current -= damage;

      OnDamaged?.Invoke(damage);

      if (_current <= 0)
      {
        OnEnded?.Invoke();
      }
    }

    public void Kill()
    {
      TakeDamage(_current);
    }
  }
}
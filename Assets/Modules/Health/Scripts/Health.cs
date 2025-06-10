using System;

namespace Modules.Health.Scripts
{
    public class Health : IHealth
    {
        public event Action OnEnded;
        public event Action<float> OnDamaged;

        private float _max;
        private float _current;

        public Health(float max, float current)
        {
            _max = max;
            _current = current;
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
    }
}

using System;
using UnityEngine;

namespace Game.Health
{
  public class HealthComponent : MonoBehaviour
  {
    public event Action OnEnded;
    
    [SerializeField] private int _maxHealth;
    
    private int _currentHealth;

    private void Awake()
    {
      _currentHealth = _maxHealth;
    }

    public void Restore()
    {
      _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
      _currentHealth -= damage;
        
      if (_currentHealth <= 0)
      {
        OnEnded?.Invoke();
      }
    }
  }
}
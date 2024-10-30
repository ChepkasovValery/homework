using System;
using Game.Health;
using Game.Move;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
  public sealed class Enemy : MonoBehaviour
  {
    public event Action<Enemy> OnHealthEnded;

    [SerializeField] private HealthComponent _health;
    [SerializeField] private MoveAgent _moveAgent;
    [SerializeField] private AttackAgent _attackAgent;
    
    private bool _canFire;

    public void Construct(BulletManager bulletManager, Transform target)
    {
      _attackAgent.Construct(bulletManager, target);
    }

    private void OnEnable()
    {
      _health.OnEnded += Die;
    }

    private void OnDisable()
    {
      _health.OnEnded -= Die;
    }

    public void RestoreHealth()
    {
      _health.Restore();
    }

    public void SetCanFire(bool canFire)
    {
      _canFire = canFire;
    }

    public void MoveTo(Vector2 endPoint)
    {
      _attackAgent.SetCanFire(false);
      
      _moveAgent.MoveTo(endPoint, () =>
      {
          _attackAgent.SetCanFire(_canFire);
      });
    }

    private void Die()
    {
      OnHealthEnded?.Invoke(this);
    }
  }
}
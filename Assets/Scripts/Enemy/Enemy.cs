using System;
using Game.Health;
using Game.Move;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class Enemy : MonoBehaviour
  {
    public bool IsReadyToFire { get; private set; }
    public event Action<Enemy> OnHealthEnded;

    [SerializeField] private HealthComponent _health;
    [SerializeField] private MoveToPointComponent _moveToPointComponent;
    [SerializeField] private EnemyGunController _enemyGunController;
    
    private bool _canFire;

    public void Construct(BulletsController bulletsController, Transform target)
    {
      _enemyGunController.Construct(bulletsController, target);
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
      IsReadyToFire = false;
      
      _moveToPointComponent.MoveTo(endPoint, () =>
      {
        if(_canFire)
          IsReadyToFire = true;
      });
    }

    private void Die()
    {
      OnHealthEnded?.Invoke(this);
    }
  }
}
using System;
using Game.Modules.CollisionWatcher.Scripts;
using Game.Modules.Damage.Scripts;
using Modules.Common;
using UnityEngine;
using Zenject;

namespace Game.Modules.Enemies.Trap.Scripts
{
  public class TrapAttackController : IInitializable, IDisposable
  {
    private readonly ICollisionWatcher _collisionWatcher;
    private readonly IDamageDealer _damageDealer;
    private readonly IEntity _entity;

    public TrapAttackController(IDamageDealer damageDealer, ICollisionWatcher collisionWatcher, IEntity entity)
    {
      _damageDealer = damageDealer;
      _collisionWatcher = collisionWatcher;
      _entity = entity;
    }

    public void Initialize()
    {
      _collisionWatcher.OnCollision += TryDealDamage;
    }

    public void Dispose()
    {
      _collisionWatcher.OnCollision -= TryDealDamage;
    }

    private void TryDealDamage(IEntity target)
    {
      if (_damageDealer.DealDamage(target))
      {
        _entity.Get<GameObject>().SetActive(false);
      }
    }
  }
}
using System;
using Modules.Common;
using Modules.Health.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Modules.Enemies.Trap.Scripts
{
  public class TrapHealthController : IInitializable, IDisposable
  {
    private readonly IHealth _health;
    private readonly IEntity _entity;

    public TrapHealthController(IEntity entity, IHealth health)
    {
      _entity = entity;
      _health = health;
    }

    public void Initialize()
    {
      _health.OnEnded += Dead;
    }

    public void Dispose()
    {
      _health.OnEnded -= Dead;
    }

    private void Dead()
    {
      _entity.Get<GameObject>().SetActive(false);
    }
  }
}
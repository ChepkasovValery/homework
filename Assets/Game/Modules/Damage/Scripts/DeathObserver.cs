using System;
using Modules.Common;
using Modules.Health.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Modules.Damage.Scripts
{
  public class DeathObserver : IInitializable, IDisposable
  {
    private readonly IHealth _health;
    private readonly IEntity _target;

    public DeathObserver(IHealth health, IEntity target)
    {
      _health = health;
      _target = target;
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
      _target.Get<GameObject>().SetActive(false);
    }
  }
}
using System.Collections.Generic;
using Game.Modules.Attacks.Api;
using Game.Modules.Attacks.Configs;
using Modules.Common;
using UnityEngine;

namespace Game.Modules.Attacks.Push.Scripts
{
  public class PushAttack : IAttack
  {
    private readonly AttackConfig _config;

    public PushAttack(AttackConfig config)
    {
      _config = config;
    }

    public bool TryAttack(List<IEntity> targets)
    {
      bool attacked = false;

      foreach (IEntity entity in targets)
      {
        if (Attack(entity))
        {
          attacked = true;
        }
      }

      return attacked;
    }

    public bool TryAttack(IEntity target) => Attack(target);

    private bool Attack(IEntity target)
    {
      if (target.TryGet(out Rigidbody2D rigidbody))
      {
        rigidbody.AddForce(Vector2.up * _config.Force);

        return true;
      }

      return false;
    }
  }
}
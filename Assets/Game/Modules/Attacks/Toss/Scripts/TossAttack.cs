using System.Collections.Generic;
using Game.Modules.Attacks.Api;
using Game.Modules.Attacks.Configs;
using Modules.Common;
using UnityEngine;

namespace Game.Modules.Attacks.Toss.Scripts
{
  public class TossAttack : IAttack
  {
    private readonly Transform _attackerTransform;
    private readonly AttackConfig _config;
    
    public TossAttack(AttackConfig config, Transform attackerTransform)
    {
      _config = config;
      _attackerTransform = attackerTransform; 
    }

    public bool TryAttack(List<IEntity> targets)
    {
      bool attacked = false;

      foreach (IEntity entity in targets)
      {
        if (TryAttack(entity))
        {
          attacked = true;
        }
      }

      return attacked;
    }

    public bool TryAttack(IEntity target)
    {
      if (target.TryGet(out Rigidbody2D rigidbody))
      {
        Vector2 attackDirection = (rigidbody.position - (Vector2)_attackerTransform.position).normalized;

        rigidbody.AddForce(attackDirection * _config.Force);

        return true;
      }

      return false;
    }
  }
}
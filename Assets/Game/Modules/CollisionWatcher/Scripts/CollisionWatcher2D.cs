using System;
using Modules.Common;
using UnityEngine;

namespace Game.Modules.CollisionWatcher.Scripts
{
  public class CollisionWatcher2D : MonoBehaviour, ICollisionWatcher
  {
    public event Action<IEntity> OnCollision;

    private void OnCollisionEnter2D(Collision2D other)
    {
      if (other.gameObject.TryGetComponent(out IEntity entity))
      {
        OnCollision?.Invoke(entity);
      }
    }
  }
}
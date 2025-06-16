using System;
using Modules.Common;

namespace Game.Modules.CollisionWatcher.Scripts
{
  public interface ICollisionWatcher
  {
    event Action<IEntity> OnCollision;
  }
}
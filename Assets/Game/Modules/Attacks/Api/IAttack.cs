using System.Collections.Generic;
using Modules.Common;

namespace Game.Modules.Attacks.Api
{
  public interface IAttack
  { 
    bool TryAttack(IEntity target);
    bool TryAttack(List<IEntity> targets);
  }
}
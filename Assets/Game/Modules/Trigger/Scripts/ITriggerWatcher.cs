using System;
using System.Collections.Generic;
using Modules.Common;

namespace Game.Modules.Trigger.Scripts
{
  public interface ITriggerWatcher
  {
    event Action<IEntity> OnTriggerEnter;
    event Action<IEntity> OnTriggerExit;
    List<IEntity> EntitiesInTrigger { get; }
  }
}
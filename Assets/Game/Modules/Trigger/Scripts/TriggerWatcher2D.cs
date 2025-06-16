using System;
using System.Collections.Generic;
using Modules.Common;
using UnityEngine;

namespace Game.Modules.Trigger.Scripts
{
  public class TriggerWatcher2D : MonoBehaviour, ITriggerWatcher
  {
    public List<IEntity> EntitiesInTrigger { get; private set; }

    public event Action<IEntity> OnTriggerEnter;
    public event Action<IEntity> OnTriggerExit;

    private void Awake()
    {
      EntitiesInTrigger = new List<IEntity>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.TryGetComponent(out EntityProvider provider))
      {
        if (!EntitiesInTrigger.Contains(provider.Entity))
        {
          EntitiesInTrigger.Add(provider.Entity);
          OnTriggerEnter?.Invoke(provider.Entity);
        }
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.TryGetComponent(out EntityProvider provider))
      {
        if (EntitiesInTrigger.Contains(provider.Entity))
        {
          EntitiesInTrigger.Remove(provider.Entity);
          OnTriggerExit?.Invoke(provider.Entity);
        }
      }
    }
  }
}
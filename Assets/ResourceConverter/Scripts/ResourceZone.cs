using System;
using UnityEngine;

namespace ResourceConverter.Scripts
{
  public class ResourceZone
  {
    public int Capacity { get; private set; }
    public int ResourceCount { get; private set; }

    public ResourceZone(int capacity, int resourceCount)
    {
      if (capacity <= 0 || resourceCount < 0 || resourceCount > capacity)
        throw new ArgumentOutOfRangeException();
      
      Capacity = capacity;
      ResourceCount = resourceCount;
    }

    public bool AddResource(int count)
    {
      if (count < 0)
        return false;

      ResourceCount = Mathf.Clamp(ResourceCount + count, 0, Capacity);

      return true;
    }
    
    public bool WithdrawResource(int count)
    {
      if (count < 0 || ResourceCount - count < 0)
        return false;
      
      ResourceCount -= count;

      return true;
    }
  }
}
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
      if(capacity <= 0)
        throw new ArgumentOutOfRangeException("Capacity must be greater than 0");
      
      if(resourceCount < 0)
        throw new ArgumentOutOfRangeException("Resource count must be greater than or equal to 0");
      
      if(resourceCount > capacity)
        throw new ArgumentOutOfRangeException("Resource count must be less than capacity");
      
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
using System.Collections.Generic;
using UnityEngine;

namespace Game.Pool
{
  public sealed class Pool<T> where T : Component
  {
    protected Queue<T> _pool = new Queue<T>();

    public void Add(T obj)
    {
      _pool.Enqueue(obj);
    }

    public bool TryGet(out T obj)
    {
      return _pool.TryDequeue(out obj);
    }
  }
}
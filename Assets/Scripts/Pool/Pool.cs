using System.Collections.Generic;
using UnityEngine;

namespace Game.Pool
{
  public sealed class Pool<T> where T : Component
  {
    private ICreator<T> _creator;
    private Transform _container;
    private Queue<T> _pool = new Queue<T>();

    private const int _prewarmCount = 10;

    public Pool(ICreator<T> creator)
    {
      _creator = creator;
      
      _container = new GameObject($"{typeof(T)} pool").transform;
      _container.gameObject.SetActive(false);
      
      Prewarm(_prewarmCount);
    }
    
    public T Get()
    {
      if (!_pool.TryDequeue(out T obj))
      {
        obj = _creator.Create();
      }

      return obj;
    }

    public void Return(T obj)
    {
      obj.transform.SetParent(_container);
      _pool.Enqueue(obj);
    }

    private void Prewarm(int prewarmCount)
    {
      for (int i = 0; i < prewarmCount; i++)
      {
        T obj = _creator.Create();
        obj.transform.SetParent(_container);
        
        _pool.Enqueue(obj);
      }
    }
  }
}
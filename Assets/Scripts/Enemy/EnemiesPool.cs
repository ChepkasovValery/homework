using Game.Pool;
using UnityEngine;

namespace ShootEmUp
{
  public class EnemiesPool : MonoBehaviour, IPool<Enemy>
  {
    [SerializeField] private EnemyCreator _enemyCreator;
    [SerializeField] private Transform _container;

    private Pool<Enemy> _pool = new Pool<Enemy>();

    public Enemy Get()
    {
      if (!_pool.TryGet(out Enemy enemy))
      {
        enemy = _enemyCreator.Create();
      }

      return enemy;
    }

    public void Return(Enemy enemy)
    {
      enemy.transform.SetParent(_container);
      _pool.Add(enemy);
    }

    public void Prewarm(int prewarmCount)
    {
      for (int i = 0; i < prewarmCount; i++)
      {
        Enemy enemy = _enemyCreator.Create();
        
        _pool.Add(enemy);
      }
    }
  }
}
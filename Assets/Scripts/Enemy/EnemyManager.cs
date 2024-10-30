using Game.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootEmUp
{
  public class EnemyManager : MonoBehaviour
  {
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private Transform[] _attackPositions;

    [SerializeField] private Transform _world;
    [SerializeField] private EnemyCreator _enemyCreator;

    private int _activeEnemiesCount;
    private Pool<Enemy> _enemyPool;

    private void Awake()
    {
      _enemyPool = new Pool<Enemy>(_enemyCreator);
    }

    public void SpawnEnemy()
    {
      Enemy enemy = _enemyPool.Get();

      ConfigureEnemy(enemy);

      if (_activeEnemiesCount < 5)
      {
        _activeEnemiesCount++;

        enemy.SetCanFire(true);
      }
      else
      {
        enemy.SetCanFire(false);
      }
    }

    private void ConfigureEnemy(Enemy enemy)
    {
      enemy.transform.SetParent(_world);
      enemy.transform.position = GetRandomPoint(_spawnPositions).position;
      enemy.RestoreHealth();
      enemy.MoveTo(GetRandomPoint(_attackPositions).position);
      
      enemy.OnHealthEnded += EnemyHealthEnded;
    }

    private void EnemyHealthEnded(Enemy enemy)
    {
      enemy.OnHealthEnded -= EnemyHealthEnded;
      
      _enemyPool.Return(enemy);
      
      _activeEnemiesCount--;
    }

    private Transform GetRandomPoint(Transform[] points)
    {
      int index = Random.Range(0, points.Length);
      
      return points[index];
    }
  }
}
using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
  public class EnemiesController : MonoBehaviour
  {
    [SerializeField] private EnemiesPool _enemyPool;

    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private Transform[] _attackPositions;

    [SerializeField] private Transform _enemiesContainer;
    [SerializeField] private Transform _world;

    private int _activeEnemiesCount;

    private void Awake()
    {
      _enemyPool.Prewarm(7);
    }

    private IEnumerator Start()
    {
      while (true)
      {
        yield return new WaitForSeconds(Random.Range(1, 2));

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
      enemy.transform.SetParent(_enemiesContainer);
      
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
using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
  public class EnemySpawnPeriod : MonoBehaviour
  {
    [SerializeField] private float _minSpawnDelay;
    [SerializeField] private float _maxSpawnDelay;
    [SerializeField] private EnemyManager _enemyManager;
    
    private IEnumerator Start()
    {
      while (true)
      {
        yield return new WaitForSeconds(Random.Range(_minSpawnDelay, _maxSpawnDelay));

        _enemyManager.SpawnEnemy();
      }
    }
  }
}
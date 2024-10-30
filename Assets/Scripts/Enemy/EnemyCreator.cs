using Game.Pool;
using UnityEngine;

namespace ShootEmUp
{
  public class EnemyCreator : MonoBehaviour, ICreator<Enemy>
  {
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _player;
    [SerializeField] private BulletManager _bulletManager;

    public Enemy Create()
    {
      Enemy enemy = Instantiate(_enemyPrefab);
      enemy.Construct(_bulletManager, _player);
      
      return enemy;
    }
  }
}
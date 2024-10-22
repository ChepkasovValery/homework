using UnityEngine;

namespace ShootEmUp
{
  public class EnemyCreator : MonoBehaviour
  {
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _player;
    [SerializeField] private BulletsController _bulletsController;

    public Enemy Create()
    {
      Enemy enemy = Instantiate(_enemyPrefab, _container);
      enemy.Construct(_bulletsController, _player);
      
      return enemy;
    }
  }
}
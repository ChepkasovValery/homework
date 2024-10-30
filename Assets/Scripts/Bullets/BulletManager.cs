using System.Collections.Generic;
using Game.Pool;
using UnityEngine;

namespace ShootEmUp
{
  public class BulletManager : MonoBehaviour
  {
    [SerializeField] private Transform _world;
    [SerializeField] private LevelBounds _levelBounds;
    [SerializeField] private BulletsCreator _bulletsCreator;

    private Pool<Bullet> _bulletsPool;
    private List<Bullet> _activeBullets = new List<Bullet>();

    private void Awake()
    {
      _bulletsPool = new Pool<Bullet>(_bulletsCreator);
    }

    private void FixedUpdate()
    {
      foreach (Bullet bullet in _activeBullets)
      {
        if (!_levelBounds.InBounds(bullet.transform.position))
        {
          ReturnBulletToPool(bullet);
          break;
        }
      }
    }

    public void SpawnBullet(int damage, Color color, int physicsLayer, Vector3 position, Vector2 velocity)
    {
      Bullet bullet = _bulletsPool.Get();
      
      bullet.transform.SetParent(_world);
      
      bullet.Init(position, velocity, damage, color, physicsLayer);
      
      _activeBullets.Add(bullet);
      
      bullet.OnCollisionEntered += BulletOnOnCollisionEntered;
    }

    private void BulletOnOnCollisionEntered(Bullet bullet, Collision2D collision)
    {
      ReturnBulletToPool(bullet);
    }
      
    private void ReturnBulletToPool(Bullet bullet)
    {
      _activeBullets.Remove(bullet);
      
      bullet.OnCollisionEntered -= BulletOnOnCollisionEntered;
      
      _bulletsPool.Return(bullet);
    }
  }
}
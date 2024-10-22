using Game.Health;
using UnityEngine;

namespace ShootEmUp
{
  public class BulletsController : MonoBehaviour
  {
    [SerializeField] private BulletsPool _bulletsPool;
    [SerializeField] private Transform _world;
   
    private void Awake()
    {
      _bulletsPool.Prewarm(10);
    }

    public void SpawnBullet(int damage, Color color, int physicsLayer, Vector3 position, Vector2 velocity)
    {
      Bullet bullet = _bulletsPool.Get();
      
      bullet.transform.SetParent(_world);
      
      bullet.Init(position, velocity, damage, color, physicsLayer);
      
      bullet.OnCollisionEntered += BulletOnOnCollisionEntered;
      bullet.OnBoundsExited += BulletBoundsExit;
    }

    private void BulletOnOnCollisionEntered(Bullet bullet, Collision2D collision)
    {
      if (collision.gameObject.TryGetComponent(out HealthComponent healthComponent))
      {
        healthComponent.TakeDamage(bullet.Damage);
      }
      
      ReturnBulletToPool(bullet);
    }

    private void BulletBoundsExit(Bullet bullet)
    {
      ReturnBulletToPool(bullet);
    }

    private void ReturnBulletToPool(Bullet bullet)
    {
      bullet.OnCollisionEntered -= BulletOnOnCollisionEntered;
      bullet.OnBoundsExited -= BulletBoundsExit;
      
      _bulletsPool.Return(bullet);
    }
  }
}
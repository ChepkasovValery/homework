using ShootEmUp;
using UnityEngine;

namespace Game.Gun
{
  public class Gun : MonoBehaviour
  {
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GunConfig _gunConfig;
    
    private BulletManager bulletManager;

    public void Construct(BulletManager bulletManager)
    {
      this.bulletManager = bulletManager;
    }
    
    public void Fire(Vector3 direction)
    {
      bulletManager.SpawnBullet(_gunConfig.Damage, 
        _gunConfig.BulletColor, 
        (int)_gunConfig.PhysicsLayer, 
        _firePoint.position, 
        direction * _gunConfig.BulletSpeed);
    }
  }
}
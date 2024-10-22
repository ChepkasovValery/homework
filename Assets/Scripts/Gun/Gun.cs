using ShootEmUp;
using UnityEngine;

namespace Game.Gun
{
  public class Gun : MonoBehaviour
  {
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GunConfig _gunConfig;
    
    private BulletsController _bulletsController;

    public void Construct(BulletsController bulletsController)
    {
      _bulletsController = bulletsController;
    }
    
    public void Fire(Vector3 direction)
    {
      _bulletsController.SpawnBullet(_gunConfig.Damage, _gunConfig.BulletColor, (int)_gunConfig.PhysicsLayer, _firePoint.position, direction * _gunConfig.BulletSpeed);
    }
  }
}
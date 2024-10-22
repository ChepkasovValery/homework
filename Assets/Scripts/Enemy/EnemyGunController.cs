using Game.Gun;
using UnityEngine;

namespace ShootEmUp
{
  public class EnemyGunController : MonoBehaviour
  {
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Gun _gun;
    [SerializeField] private float _fireDelay;
    
    private float _cooldown;
    private Transform _target;

    public void Construct(BulletsController bulletsController, Transform target)
    {
      _target = target;
      _gun.Construct(bulletsController);
    }
    
    private void FixedUpdate()
    {
      if (_enemy.IsReadyToFire)
      {
        _cooldown -= Time.deltaTime;

        if (_cooldown <= 0)
        {
          _cooldown = _fireDelay;
          _gun.Fire((_target.position - transform.position).normalized);
        }
      }
    }
  }
}
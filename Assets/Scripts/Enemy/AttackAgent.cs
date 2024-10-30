using Game.Gun;
using UnityEngine;

namespace ShootEmUp
{
  public class AttackAgent : MonoBehaviour
  {
    [SerializeField] private Gun _gun;
    [SerializeField] private float _fireDelay;
    
    private float _cooldown;
    private Transform _target;
    private bool _canFire;

    public void Construct(BulletManager bulletManager, Transform target)
    {
      _target = target;
      _gun.Construct(bulletManager);
    }

    public void SetCanFire(bool canFire)
    {
      _canFire = canFire;
    }
    
    private void FixedUpdate()
    {
      if (_canFire)
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
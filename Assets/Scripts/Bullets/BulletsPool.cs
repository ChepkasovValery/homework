using Game.Pool;
using UnityEngine;

namespace ShootEmUp
{
  public class BulletsPool : MonoBehaviour, IPool<Bullet>
  {
    [SerializeField] private BulletsCreator _bulletsCreator;
    [SerializeField] private Transform _container;

    private Pool<Bullet> _pool = new Pool<Bullet>();

    public Bullet Get()
    {
      if (!_pool.TryGet(out Bullet bullet))
      {
        bullet =  _bulletsCreator.CreateBullet();
      }

      return bullet;
    }

    public void Return(Bullet bullet)
    {
      bullet.transform.SetParent(_container);
      _pool.Add(bullet);
    }

    public void Prewarm(int prewarmCount)
    {
      for (int i = 0; i < prewarmCount; i++)
      {
        Bullet bullet = _bulletsCreator.CreateBullet();

        _pool.Add(bullet);
      }
    }
  }
}
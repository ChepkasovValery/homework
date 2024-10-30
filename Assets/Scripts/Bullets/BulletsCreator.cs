using Game.Pool;
using UnityEngine;

namespace ShootEmUp
{
  public class BulletsCreator : MonoBehaviour, ICreator<Bullet>
  {
    [SerializeField] private Bullet _bulletPrefab;

    public Bullet Create()
    {
      return Instantiate(_bulletPrefab);;
    }
  }
}
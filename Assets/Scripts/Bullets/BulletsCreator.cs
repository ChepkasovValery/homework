using UnityEngine;

namespace ShootEmUp
{
  public class BulletsCreator : MonoBehaviour
  {
    [SerializeField] private Transform _container;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private LevelBounds _levelBounds;

    public Bullet CreateBullet()
    {
      Bullet bullet = Instantiate(_bulletPrefab, _container);
      bullet.Construct(AddBoundsChecker(bullet));

      return bullet;
    }
    
    private BoundsChecker AddBoundsChecker(Bullet bullet)
    {
      BoundsChecker boundsChecker = bullet.gameObject.AddComponent<BoundsChecker>();
      boundsChecker.Construct(_levelBounds);
      
      return boundsChecker;
    }
  }
}
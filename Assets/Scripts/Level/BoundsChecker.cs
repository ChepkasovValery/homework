using System;
using UnityEngine;

namespace ShootEmUp
{
  public class BoundsChecker : MonoBehaviour
  {
    public event Action OnBoundsExit;
    
    private LevelBounds _levelBounds;

    public void Construct(LevelBounds levelBounds)
    {
      _levelBounds = levelBounds;
    }
    
    private void Update()
    {
      if (!_levelBounds.InBounds(transform.position))
      {
        OnBoundsExit?.Invoke();
      }
    }
  }
}
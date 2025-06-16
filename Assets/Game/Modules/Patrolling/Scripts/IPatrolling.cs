using System;
using UnityEngine;

namespace Game.Modules.Patrolling.Scripts
{
  public interface IPatrolling
  {
    event Action<Vector3> OnDestinationChanged; 
    void Start();
    void Stop();
  }
}
using System;
using Game.Modules.Moving.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Modules.Patrolling.Scripts
{
  public class Patrolling : IPatrolling, ITickable, IInitializable
  {
    public event Action<Vector3> OnDestinationChanged;
    
    private readonly IMover _mover;
    private readonly Vector3[] _waypoints;

    private int _currentWaypointIndex;
    private bool _patrolling;

    public Patrolling(Vector3[] waypoints, IMover mover)
    {
      _waypoints = waypoints;
      _mover = mover;
    }

    public void Initialize()
    {
      SetNextWaypoint();
      
      Start();
    }

    public void Start()
    {
      _patrolling = true;
    }

    public void Stop()
    {
      _patrolling = false;
    }

    public void Tick()
    {
      if(!_patrolling)
        return;
      
      if (Vector3.Distance(_mover.Position, _waypoints[_currentWaypointIndex]) > 0.1f)
      {
        Vector3 direction = (_waypoints[_currentWaypointIndex] - _mover.Position).normalized;
        
        _mover.Move(direction);
      }
      else
      {
        SetNextWaypoint();
      }
    }

    private void SetNextWaypoint()
    {
      _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
      
      OnDestinationChanged?.Invoke(_waypoints[_currentWaypointIndex]);
    }
  }
}
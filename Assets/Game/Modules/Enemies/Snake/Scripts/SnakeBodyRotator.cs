using System;
using Game.Modules.Moving.Scripts;
using Game.Modules.Patrolling.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Modules.Enemies.Snake.Scripts
{
  public class SnakeBodyRotator : IInitializable, IDisposable
  {
    private readonly IBodyRotator _bodyRotator;
    private readonly IPatrolling _patrolling;
    private readonly Transform _transform;

    public SnakeBodyRotator(IBodyRotator bodyRotator, IPatrolling patrolling, Transform transform)
    {
      _bodyRotator = bodyRotator;
      _patrolling = patrolling;
      _transform = transform;
    }

    public void Initialize()
    {
      _patrolling.OnDestinationChanged += Rotate;
    }

    public void Dispose()
    {
      _patrolling.OnDestinationChanged -= Rotate;
    }

    private void Rotate(Vector3 targetPosition)
    {
      Vector3 direction = (targetPosition - _transform.position).normalized;
      direction.y = direction.z = 0;
      
      _bodyRotator.LookToDirection(direction);
    }
  }
}
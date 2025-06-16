using Game.Modules.Moving.Configs;
using UnityEngine;

namespace Game.Modules.Moving.Scripts
{
  public class RigidbodyVelocityMover : IMover
  {
    public Vector3 Position => _rigidbody.position;
    
    private readonly Rigidbody2D _rigidbody;
    private readonly MoveConfig _moveConfig;
    
    public RigidbodyVelocityMover(Rigidbody2D rigidbody, MoveConfig moveConfig)
    {
      _rigidbody = rigidbody;
      _moveConfig = moveConfig;
    }

    public void Move(Vector2 velocity)
    {
      _rigidbody.velocity = velocity * _moveConfig.Speed;
    }
  }
}
using Game.Modules.Moving.Configs;
using UnityEngine;

namespace Game.Modules.Moving.Scripts
{
  public class RigidbodyForceMover : IMover
  {
    public Vector3 Position => _rigidbody.position;
    
    private readonly Rigidbody2D _rigidbody;
    private readonly MoveConfig _moveConfig;

    public RigidbodyForceMover(Rigidbody2D rigidbody, MoveConfig moveConfig)
    {
      _rigidbody = rigidbody;
      _moveConfig = moveConfig;
    }

    public void Move(Vector2 velocity)
    {
      float targetVelocityX = velocity.x * _moveConfig.Speed;
      float velocityChangeX = targetVelocityX - _rigidbody.velocity.x;
      float forceX = Mathf.Clamp(velocityChangeX * _rigidbody.mass / Time.fixedDeltaTime, -_moveConfig.Acceleration, _moveConfig.Acceleration);

      _rigidbody.AddForce(new Vector2(forceX, 0));
    }
  }
}
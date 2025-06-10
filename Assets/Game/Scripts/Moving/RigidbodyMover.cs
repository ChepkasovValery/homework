using UnityEngine;

namespace Game.Scripts.Moving
{
  public class RigidbodyMover : IMover
  {
    private readonly Rigidbody2D _rigidbody;
    private readonly MoveConfig _moveConfig;

    public RigidbodyMover(Rigidbody2D rigidbody, MoveConfig moveConfig)
    {
      _rigidbody = rigidbody;
      _moveConfig = moveConfig;
    }

    public void Move(Vector2 velocity)
    {
      _rigidbody.velocity = new Vector2(velocity.x * _moveConfig.Speed, _rigidbody.velocity.y);
    }

    public void Jump()
    {
      _rigidbody.AddForce(Vector2.up * _moveConfig.JumpForce);
    }
  }
}
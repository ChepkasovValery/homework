using Game.Configs;
using UnityEngine;

namespace Game.Move
{
  public class MoveComponent : MonoBehaviour
  {
    [SerializeField] private MoveConfig _moveConfig;
    [SerializeField] private Rigidbody2D _rigidbody;
    
    public void Move(Vector2 direction)
    {
      Vector2 moveStep = direction * Time.fixedDeltaTime * _moveConfig.Speed;
      Vector2 targetPosition = _rigidbody.position + moveStep;
      _rigidbody.MovePosition(targetPosition);
    }
  }
}
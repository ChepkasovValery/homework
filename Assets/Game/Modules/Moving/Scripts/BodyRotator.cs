using UnityEngine;
using Zenject;

namespace Game.Modules.Moving.Scripts
{
  public class BodyRotator : IBodyRotator, IInitializable
  {
    private readonly Transform _body;

    private Vector3 _scale;
    private float _startScaleX;

    public BodyRotator(Transform body)
    {
      _body = body;
    }

    public void Initialize()
    {
      _startScaleX = _body.localScale.x;
      _scale = _body.localScale;
    }

    public void LookToDirection(Vector2 direction)
    {
      if (direction.x < -Mathf.Epsilon)
      {
        _scale.x = -_startScaleX;
      }
      else if(direction.x > Mathf.Epsilon)
      {
        _scale.x = _startScaleX;
      }
      
      _body.localScale = _scale;
    }
  }
}
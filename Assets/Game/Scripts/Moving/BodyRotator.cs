using Game.Scripts.Input;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Moving
{
  public class BodyRotator : ITickable, IInitializable
  {
    private readonly IInput _input;
    private readonly Transform _body;

    private Vector3 _scale;
    private float _startScaleX;

    public BodyRotator(IInput input, Transform body)
    {
      _input = input;
      _body = body;
    }

    public void Initialize()
    {
      _startScaleX = _body.localScale.x;
      _scale = _body.localScale;
    }

    public void Tick()
    {
      if (_input.MoveDirection.x < -Mathf.Epsilon)
      {
        _scale.x = -_startScaleX;
      }
      else if(_input.MoveDirection.x > Mathf.Epsilon)
      {
        _scale.x = _startScaleX;
      }
      
      _body.localScale = _scale;
    }
  }
}
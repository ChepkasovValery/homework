using System;
using Game.Modules.Moving.Configs;
using UnityEngine;

namespace Game.Modules.Moving.Scripts
{
  public class RigidbodyJumper : IJumper
  {
    public event Action OnJumped;
    
    private readonly JumpConfig _config;
    private readonly Rigidbody2D _rigidbody;

    public RigidbodyJumper(JumpConfig config, Rigidbody2D rigidbody)
    {
      _config = config;
      _rigidbody = rigidbody;
    }

    public void Jump()
    {
      _rigidbody.AddForce(Vector2.up * _config.Force);
      
      OnJumped?.Invoke();
    }
  }
}
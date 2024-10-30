using System;
using Game.Health;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class Bullet : MonoBehaviour
  {
    public event Action<Bullet, Collision2D> OnCollisionEntered;

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private int _damage;
      
    public void Init(Vector3 position, Vector2 velocity, int damage, Color color, int physicsLayer)
    {
      _damage = damage;
      _spriteRenderer.color = color;
      gameObject.layer = physicsLayer;
      _rigidbody2D.velocity = velocity;
      transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.TryGetComponent(out HealthComponent healthComponent))
      {
        healthComponent.TakeDamage(_damage);
      }
      
      OnCollisionEntered?.Invoke(this, collision);
    }
  }
}
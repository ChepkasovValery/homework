using System;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class Bullet : MonoBehaviour
  {
    public event Action<Bullet, Collision2D> OnCollisionEntered;
    public event Action<Bullet> OnBoundsExited;

    public int Damage { get; private set; }

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private BoundsChecker _boundsChecker;

    public void Construct(BoundsChecker boundsChecker)
    {
      _boundsChecker = boundsChecker;
    }

    private void OnEnable()
    {
      _boundsChecker.OnBoundsExit += BoundsExited;
    }

    private void OnDisable()
    {
      _boundsChecker.OnBoundsExit -= BoundsExited;
    }

    public void Init(Vector3 position, Vector2 velocity, int damage, Color color, int physicsLayer)
    {
      Damage = damage;
      _spriteRenderer.color = color;
      gameObject.layer = physicsLayer;
      _rigidbody2D.velocity = velocity;
      transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      OnCollisionEntered?.Invoke(this, collision);
    }

    private void BoundsExited()
    {
      OnBoundsExited?.Invoke(this);
    }
  }
}
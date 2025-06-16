using System;
using System.Collections;
using Game.Modules.Animations.Damage.Configs;
using Modules.Common;
using Modules.Health.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Modules.Animations.Damage.Scripts
{
  public class DamageAnimation : IInitializable, IDisposable
  {
    private readonly Entity _entity;
    private readonly SpriteRenderer _spriteRenderer;
    private readonly IHealth _health;
    private readonly DamageAnimationConfig _config;

    private Color _startColor;
    private Coroutine _animationCoroutine;

    public DamageAnimation(Entity entity, IHealth health, SpriteRenderer spriteRenderer, DamageAnimationConfig config)
    {
      _entity = entity;
      _health = health;
      _spriteRenderer = spriteRenderer;
      _config = config;
    }

    public void Initialize()
    {
      _startColor = _spriteRenderer.color;
      
      _health.OnDamaged += Play;
    }

    public void Dispose()
    {
      _health.OnDamaged -= Play;
    }

    public void Play(float damage)
    {
      if (_animationCoroutine != null)
      {
        _entity.StopCoroutine(_animationCoroutine);
        _animationCoroutine = null;
      }
      
      if(_health.IsAlive)
        _animationCoroutine = _entity.StartCoroutine(DamageAnim());
    }

    private IEnumerator DamageAnim()
    {
      float cycleDuration = _config.DamageDuration / _config.DamageCycleCount / 2;

      for (int i = 0; i < _config.DamageCycleCount; i++)
      {
        for (float t = 0; t < cycleDuration; t += Time.deltaTime)
        {
          _spriteRenderer.color = Color.Lerp(_startColor, _config.DamageColor, t);

          yield return null;
        }
        
        for (float t = 0; t < cycleDuration; t += Time.deltaTime)
        {
          _spriteRenderer.color = Color.Lerp(_config.DamageColor, _startColor, t);

          yield return null;
        }
      }
      
      _spriteRenderer.color = _startColor;
    }
  }
}
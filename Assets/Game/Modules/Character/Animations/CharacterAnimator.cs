using System;
using System.Collections;
using Game.Modules.Character.Animations.Configs;
using Game.Modules.Moving.Scripts;
using Modules.Common;
using UnityEngine;
using Zenject;

namespace Game.Modules.Character.Animations
{
  public class CharacterAnimator : IInitializable, IDisposable
  {
    private readonly IJumper _jumper;
    private readonly Entity _entity;
    private readonly CharacterAnimationsConfig _config;
    private readonly Transform _targetTransform;

    private Vector3 _startScale;

    public CharacterAnimator(Entity entity, Transform targetTransform, CharacterAnimationsConfig config, IJumper jumper)
    {
      _entity = entity;
      _config = config;
      _jumper = jumper;
      _targetTransform = targetTransform;
    }

    public void Initialize()
    {
      _startScale = _targetTransform.localScale;

      _jumper.OnJumped += PlayJumpAnim;
    }

    public void Dispose()
    {
      _jumper.OnJumped -= PlayJumpAnim;
    }

    private void PlayJumpAnim()
    {
      _entity.StartCoroutine(JumpAnim());
    }
    
    private IEnumerator JumpAnim()
    {
      float halfDuration = _config.JumpDuration / 2;
      float elapsedTime = 0f;

      while (elapsedTime < halfDuration)
      {
        _targetTransform.localScale = Vector3.Lerp(_startScale, _config.JumpScale, elapsedTime / halfDuration);

        elapsedTime += Time.deltaTime;

        yield return null;
      }

      elapsedTime = 0f;

      while (elapsedTime < halfDuration)
      {
        _targetTransform.localScale = Vector3.Lerp(_config.JumpScale, _startScale, elapsedTime / halfDuration);

        elapsedTime += Time.deltaTime;

        yield return null;
      }
    }
  }
}
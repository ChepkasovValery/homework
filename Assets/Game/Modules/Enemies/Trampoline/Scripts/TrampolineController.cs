using System;
using Game.Modules.Attacks.Api;
using Game.Modules.Attacks.Push.Scripts;
using Game.Modules.SoundSystem.Scripts;
using Game.Modules.Trigger.Scripts;
using Modules.Common;
using UnityEngine;
using Zenject;

namespace Game.Modules.Enemies.Trampoline.Scripts
{
  public class TrampolineController : IInitializable, IDisposable
  {
    private readonly ITriggerWatcher _triggerWatcher;
    private readonly IAttack _pushAttack;
    private readonly ISoundManager _soundManager;
    
    public TrampolineController(ITriggerWatcher triggerWatcher, IAttack pushAttack, ISoundManager soundManager)
    {
      _triggerWatcher = triggerWatcher;
      _pushAttack = pushAttack;
      _soundManager = soundManager;
    }

    public void Initialize()
    {
      _triggerWatcher.OnTriggerEnter += TryAttack;
    }

    public void Dispose()
    {
      _triggerWatcher.OnTriggerEnter -= TryAttack;
    }

    private void TryAttack(IEntity target)
    {
      if (target.TryGet(out Rigidbody2D rigidbody))
      {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);  
        
        if (_pushAttack.TryAttack(target))
        {
          _soundManager.PlayOneShot(_soundManager.Sounds.Trampoline);
        }
      }
    }
  }
}
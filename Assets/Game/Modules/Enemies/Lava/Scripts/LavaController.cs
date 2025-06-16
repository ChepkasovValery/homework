using System;
using Game.Modules.SoundSystem.Scripts;
using Game.Modules.Trigger.Scripts;
using Modules.Common;
using Modules.Health.Scripts;
using Zenject;

namespace Game.Modules.Enemies.Lava.Scripts
{
  public class LavaController : IInitializable, IDisposable
  {
    private readonly ITriggerWatcher _triggerWatcher;
    private readonly ISoundManager _soundManager;

    public LavaController(ITriggerWatcher triggerWatcher, ISoundManager soundManager)
    {
      _triggerWatcher = triggerWatcher;
      _soundManager = soundManager;
    }

    public void Initialize()
    {
      _triggerWatcher.OnTriggerEnter += TryKill;
    }

    public void Dispose()
    {
      _triggerWatcher.OnTriggerEnter -= TryKill;
    }

    private void TryKill(IEntity target)
    {
      if (target.TryGet(out IHealth health))
      {
        health.Kill();
        
        _soundManager.PlayOneShot(_soundManager.Sounds.Lava);
      }
    }
  }
}
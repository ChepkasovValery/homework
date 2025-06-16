using Game.Modules.Attacks.Configs;
using Game.Modules.Attacks.Push.Scripts;
using Game.Modules.Enemies.Trampoline.Scripts;
using Game.Modules.Trigger.Scripts;
using Modules.Common;
using UnityEngine;
using Zenject;

namespace Game.Modules.Enemies.Trampoline.Installer
{
  public class TrampolineInstaller : MonoInstaller
  {
    [SerializeField] private AttackConfig _attackConfig;
    [SerializeField] private TriggerWatcher2D _triggerWatcher2D;
    
    public override void InstallBindings()
    {
      Container.BindInterfacesAndSelfTo<Entity>().FromComponentOnRoot().AsSingle();

      Container.BindInterfacesTo<TriggerWatcher2D>().FromInstance(_triggerWatcher2D).AsSingle();
      Container.BindInterfacesTo<PushAttack>().AsSingle().WithArguments(_attackConfig);
      
      Container.BindInterfacesTo<TrampolineController>().AsSingle();
    }
  }
}
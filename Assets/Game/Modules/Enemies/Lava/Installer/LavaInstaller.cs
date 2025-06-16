using Game.Modules.Enemies.Lava.Scripts;
using Game.Modules.Trigger.Scripts;
using Modules.Common;
using Zenject;

namespace Game.Modules.Enemies.Lava.Installer
{
  public class LavaInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.BindInterfacesAndSelfTo<Entity>().FromComponentOnRoot().AsSingle();

      Container.BindInterfacesTo<TriggerWatcher2D>().FromNewComponentOnRoot().AsSingle();

      Container.BindInterfacesTo<LavaController>().AsSingle().NonLazy();
    }
  }
}
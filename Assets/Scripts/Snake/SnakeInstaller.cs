using Game;
using Snake;

namespace Zenject
{
  public class SnakeInstaller : Installer<SnakeInstaller>
  {
    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<Modules.Snake>()
        .FromComponentsInHierarchy()
        .AsSingle();

      Container
        .BindInterfacesTo<SnakeSelfCollidedObserver>()
        .AsSingle();
      
       Container
        .BindInterfacesTo<SnakeOutOfBoundsObserver>()
        .AsSingle();
      
      Container
        .BindInterfacesTo<SnakeMover>()
        .AsSingle();
      
      Container
        .BindInterfacesTo<SnakeExpander>()
        .AsSingle();

      Container
        .BindInterfacesTo<SnakeSpeedChanger>()
        .AsSingle();

      Container
        .BindInterfacesTo<SnakeCoinCollector>()
        .AsSingle();
    }
  }
}
using Game;
using Modules;

namespace Zenject
{
  public class SnakeInstaller : Installer<SnakeInstaller>
  {
    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<Input>()
        .AsSingle();
        
      Container
        .BindInterfacesTo<Snake>()
        .FromComponentsInHierarchy()
        .AsSingle();

      Container
        .BindInterfacesTo<SnakeMoveController>()
        .AsSingle();
      
      Container
        .BindInterfacesTo<SnakeExpander>()
        .AsSingle();
    }
  }
}
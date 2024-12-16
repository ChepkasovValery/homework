using Game.World;
using SnakeGame;

namespace Zenject
{
  public class WorldInstaller : Installer<WorldInstaller>
  {
    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<World>()
        .FromComponentsInHierarchy()
        .AsSingle();

      Container
        .BindInterfacesTo<WorldBounds>()
        .FromComponentsInHierarchy()
        .AsSingle();
    }
  }
}
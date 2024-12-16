using Game.UI;
using SnakeGame;

namespace Zenject
{
  public class UIInstaller : Installer<UIInstaller>
  {
    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<GameUI>()
        .FromComponentsInHierarchy()
        .AsSingle();
      
      Container
        .BindInterfacesTo<UIController>()
        .AsSingle();
    }
  }
}
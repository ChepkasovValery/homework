using GameState;

namespace Zenject
{
  public class GameStateInstaller : Installer<GameStateInstaller>
  {
    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<GameCycle>()
        .AsSingle();
    }
  }
}
using Game;
using Game.GameState;
using Modules;

namespace Zenject
{
  public class GameStateInstaller : Installer<int, GameStateInstaller>
  {
    [Inject] private int _levelsCount;
    
    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<GameOverObserver>()
        .AsSingle();

      Container
        .BindInterfacesTo<Difficulty>()
        .AsSingle()
        .WithArguments(_levelsCount);

      Container
        .BindInterfacesTo<LevelController>()
        .AsSingle();
    }
  }
}
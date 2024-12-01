using Game.Player;
using Modules;

namespace Zenject
{
  public class ScoresInstaller : Installer<ScoresInstaller>
  {
    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<PlayerScoresController>()
        .AsSingle();
        
      Container
        .BindInterfacesTo<Score>()
        .AsSingle();
    }
  }
}
using Modules;
using Zenject;

namespace Game
{
  public class DifficultyInstaller : Installer<int, DifficultyInstaller>
  {
    [Inject] private int _levelsCount;

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<Difficulty>()
        .AsSingle()
        .WithArguments(_levelsCount);

      Container
        .BindInterfacesTo<DifficultyChanger>()
        .AsSingle();

      Container
        .BindInterfacesTo<DifficultyMaxObserver>()
        .AsSingle();
    }
  }
}
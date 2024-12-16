using Coins;
using Game.Coins;
using Game.World;
using Modules;

namespace Zenject
{
  public class CoinInstaller : Installer<Coin, IWorld, CoinInstaller>
  {
    [Inject] private Coin _coinPrefab;
    [Inject] private IWorld _world;

    public override void InstallBindings()
    {
      Container
        .BindMemoryPool<Coin, CoinPool>()
        .WithInitialSize(10)
        .FromComponentInNewPrefab(_coinPrefab)
        .WithGameObjectName("Coin")
        .UnderTransform(_world.Value)
        .AsSingle();

      Container
        .BindInterfacesTo<CoinManager>()
        .AsSingle();

      Container
        .BindInterfacesTo<LevelCoinSpawner>()
        .AsSingle();
    }
  }
}
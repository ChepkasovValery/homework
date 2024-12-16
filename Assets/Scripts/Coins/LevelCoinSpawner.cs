using System;
using Game.Coins;
using Modules;
using Zenject;

namespace Coins
{
  public class LevelCoinSpawner : IInitializable, IDisposable
  {
    private readonly ICoinManager _coinManager;
    private readonly IDifficulty _difficulty;

    public LevelCoinSpawner(IDifficulty difficulty, ICoinManager coinManager)
    {
      _difficulty = difficulty;
      _coinManager = coinManager;
    }

    public void Initialize()
    {
      _difficulty.OnStateChanged += SpawnCoins;
    }

    public void Dispose()
    {
      _difficulty.OnStateChanged -= SpawnCoins;
    }

    private void SpawnCoins()
    {
      _coinManager.SpawnCoins(_difficulty.Current);
    }
  }
}
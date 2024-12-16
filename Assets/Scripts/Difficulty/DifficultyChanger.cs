using System;
using Game.Coins;
using Modules;
using Zenject;

namespace Game
{
  public class DifficultyChanger : IInitializable, IDisposable
  {
    private readonly IDifficulty _difficulty;
    private readonly ICoinManager _coinManager;

    public DifficultyChanger(IDifficulty difficulty, ICoinManager coinManager)
    {
      _coinManager = coinManager;
      _difficulty = difficulty;
    }
      
    public void Initialize()
    {
      _coinManager.OnAllCoinsCollected += Increase;
    }

    public void Dispose()
    {
      _coinManager.OnAllCoinsCollected -= Increase;
    }

    private void Increase()
    {
      _difficulty.Next(out _);
    }
  }
}
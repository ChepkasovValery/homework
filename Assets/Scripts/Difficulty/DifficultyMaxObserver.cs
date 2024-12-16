using System;
using Game.Coins;
using GameState;
using Modules;
using Zenject;

namespace Game
{
  public class DifficultyMaxObserver : IInitializable, IDisposable
  {
    private readonly ICoinManager _coinManager;
    private readonly IDifficulty _difficulty;
    private readonly IGameCycle _gameCycle;

    public DifficultyMaxObserver(ICoinManager coinManager, IGameCycle gameCycle, IDifficulty difficulty)
    {
      _gameCycle = gameCycle;
      _difficulty = difficulty;
      _coinManager = coinManager;
    }

    public void Initialize()
    {
      _coinManager.OnAllCoinsCollected += Check;
    }

    public void Dispose()
    {
      _coinManager.OnAllCoinsCollected -= Check;
    }
    
    private void Check()
    {
      if (_difficulty.Current == _difficulty.Max)
      {
        _gameCycle.Win();
      }
    }
  }
}
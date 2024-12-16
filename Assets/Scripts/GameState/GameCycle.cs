using System;
using Game.GameState;
using Modules;
using Zenject;

namespace GameState
{
  public class GameCycle : IGameOverObservable, IGameCycle, IInitializable
  {
    public event Action OnWin;
    public event Action OnLose;

    private readonly IDifficulty _difficulty;

    public GameCycle(IDifficulty difficulty)
    {
      _difficulty = difficulty;
    }

    public void Win()
    {
      OnWin?.Invoke();
    }

    public void Lose()
    {
      OnLose?.Invoke();
    }

    public void Initialize()
    {
      StartGame();
    }

    private void StartGame()
    {
      _difficulty.Next(out _);
    }
  }
}
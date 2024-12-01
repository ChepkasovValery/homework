using System;

namespace Game.GameState
{
  public interface IGameOverObserver
  {
    event Action OnWin;
    event Action OnLoose;
  }
}
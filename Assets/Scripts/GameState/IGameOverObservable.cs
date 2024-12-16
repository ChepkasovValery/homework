using System;

namespace Game.GameState
{
  public interface IGameOverObservable
  {
    event Action OnWin;
    event Action OnLose;
  }
}
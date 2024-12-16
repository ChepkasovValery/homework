using System;
using Game.GameState;
using Modules;
using SnakeGame;
using Zenject;

namespace Game.UI
{
  public class UIController : IInitializable, IDisposable
  {
    private readonly IScore _playerScores;
    private readonly IGameUI _gameUI;
    private readonly IDifficulty _difficulty;
    private readonly IGameOverObservable gameOverObservable;

    public UIController(IScore playerScores, IGameUI gameUI, IDifficulty difficulty, 
      IGameOverObservable gameOverObservable)
    {
      this.gameOverObservable = gameOverObservable;
      _playerScores = playerScores;
      _gameUI = gameUI;
      _difficulty = difficulty;
    }

    public void Initialize()
    {
      _playerScores.OnStateChanged += UpdateScoresView;
      _difficulty.OnStateChanged += UpdateLevelView;
      gameOverObservable.OnWin += ShowWin;
      gameOverObservable.OnLose += ShowLoose;
      
      UpdateScoresView(_playerScores.Current);
      UpdateLevelView();
    }

    public void Dispose()
    {
      _playerScores.OnStateChanged -= UpdateScoresView;
      _difficulty.OnStateChanged -= UpdateLevelView;
      gameOverObservable.OnWin -= ShowWin;
      gameOverObservable.OnLose -= ShowLoose;
    }

    private void UpdateScoresView(int scores)
    {
      _gameUI.SetScore(scores.ToString());
    }

    private void UpdateLevelView()
    {
      _gameUI.SetDifficulty(_difficulty.Current, _difficulty.Max);
    }

    private void ShowWin()
    {
      _gameUI.GameOver(true);
    }

    private void ShowLoose()
    {
      _gameUI.GameOver(false);
    }
  }
}
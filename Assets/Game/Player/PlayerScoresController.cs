using System;
using Game.Coins;
using Modules;
using Zenject;

namespace Game.Player
{
  public class PlayerScoresController : IInitializable, IDisposable
  {
    private readonly ICoinCollector _coinCollector;
    private readonly IScore _score;

    public PlayerScoresController(ICoinCollector coinCollector, IScore score)
    {
      _score = score;
      _coinCollector = coinCollector;
    }

    public void Initialize()
    {
      _coinCollector.OnCoinCollected += AddScore;
    }

    public void Dispose()
    {
      _coinCollector.OnCoinCollected -= AddScore;
    }

    private void AddScore(ICoin coin)
    {
      _score.Add(coin.Score);
    }
  }
}
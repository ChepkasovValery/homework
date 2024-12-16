using System;
using Game.Coins;
using Modules;
using Snake;
using Zenject;

namespace Game.Player
{
  public class ScoreController : IInitializable, IDisposable
  {
    private readonly ISnakeCoinCollector _coinCollector;
    private readonly IScore _score;

    public ScoreController(IScore score, ISnakeCoinCollector coinCollector)
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
using System;
using Game.Coins;
using Modules;
using Zenject;

namespace Game
{
  public class SnakeExpander : IInitializable, IDisposable
  {
    private readonly ISnake _snake;
    private readonly ICoinCollector _coinCollector;

    public SnakeExpander(ISnake snake, ICoinCollector coinCollector)
    {
      _snake = snake;
      _coinCollector = coinCollector;
    }

    public void Initialize()
    {
      _coinCollector.OnCoinCollected += Expand;
    }

    public void Dispose()
    {
      _coinCollector.OnCoinCollected -= Expand;
    }

    private void Expand(ICoin coin)
    {
      _snake.Expand(coin.Bones);
    }
  }
}
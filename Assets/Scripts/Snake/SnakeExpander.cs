using System;
using Modules;
using Snake;
using Zenject;

namespace Game
{
  public class SnakeExpander : IInitializable, IDisposable
  {
    private readonly ISnake _snake;
    private readonly ISnakeCoinCollector _coinCollector;

    public SnakeExpander(ISnake snake, ISnakeCoinCollector coinCollector)
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
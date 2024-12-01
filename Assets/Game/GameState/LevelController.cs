using System;
using Game.Coins;
using Modules;
using SnakeGame;
using Zenject;

namespace Game
{
  public class LevelController : IInitializable, IDisposable
  {
    private readonly ICoinSpawner _coinSpawner;
    private readonly IDifficulty _difficulty;
    private readonly IWorldBounds _worldBounds;
    private readonly ISnake _snake;
    private readonly ICoinCollector _coinCollector;

    public LevelController(IDifficulty difficulty, ICoinSpawner coinSpawner, IWorldBounds worldBounds, ISnake snake,
      ICoinCollector coinCollector)
    {
      _coinCollector = coinCollector;
      _snake = snake;
      _worldBounds = worldBounds;
      _difficulty = difficulty;
      _coinSpawner = coinSpawner;
    }
      
    public void Initialize()
    {
      _coinCollector.OnAllCoinsCollected += NextLevel;
      
      NextLevel();
    }

    public void Dispose()
    {
      _coinCollector.OnAllCoinsCollected -= NextLevel;
    }

    private void NextLevel()
    {
      _difficulty.Next(out int difficulty);
      
      for (int i = 0; i < difficulty; i++)
      {
        _coinSpawner.Spawn(_worldBounds.GetRandomPosition());
      }
      
      _snake.SetSpeed(difficulty);
    }
  }
}
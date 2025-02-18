using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Modules.SaveLoadSystem;
using Zenject;

namespace Game.Scripts.SaveLoadSystem
{
  public class GameSaveLoader : IInitializable
  {
    private readonly IGameRepository _gameRepository;
    private readonly EntitySerializer _entitySerializer;

    public GameSaveLoader(IGameRepository gameRepository, EntitySerializer entitySerializer)
    {
      _entitySerializer = entitySerializer;
      _gameRepository = gameRepository;
    }

    public void Initialize()
    {
    }

    public async UniTaskVoid Save(Action<bool, int> callback)
    {
      (bool, int) result = await _gameRepository.SetState(SerializeCurrentState());

      callback?.Invoke(result.Item1, result.Item2);
    }

    public async UniTaskVoid Load(int version, Action<bool, int> callback)
    {
      (bool, int, IDictionary<string, string>) result = await _gameRepository.GetState(version);

      if (result.Item1)
      {
        _entitySerializer.Deserialize(result.Item3);
      }

      callback?.Invoke(result.Item1, result.Item2);
    }

    private Dictionary<string, string> SerializeCurrentState()
    {
      Dictionary<string, string> currentState = new Dictionary<string, string>();

      _entitySerializer.Serialize(currentState);

      return currentState;
    }
  }
}
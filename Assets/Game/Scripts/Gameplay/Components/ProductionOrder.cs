using System.Collections.Generic;
using Game.Scripts.SaveLoadSystem;
using Modules.Entities;
using Newtonsoft.Json;
using UnityEngine;

namespace SampleGame.Gameplay
{
  //Can be extended
  public sealed class ProductionOrder : MonoBehaviour, IComponentSerializer
  {
    ///Variable
    [SerializeField] private List<EntityConfig> _queue;

    public IReadOnlyList<EntityConfig> Queue
    {
      get => _queue;
      set => _queue = new List<EntityConfig>(value);
    }

    public void Serialize(IDictionary<string, string> state)
    {
      state[nameof(ProductionOrder)] = JsonConvert.SerializeObject(
        new Dictionary<string, List<EntityConfig>>
        {
          { nameof(_queue), _queue }
        });
    }

    public void Deserialize(IDictionary<string, string> state)
    {
      string json = state[nameof(ProductionOrder)];

      var data = JsonConvert.DeserializeObject<Dictionary<string, List<EntityConfig>>>(json);

      if (data != null) _queue = data[nameof(_queue)];
    }
  }
}
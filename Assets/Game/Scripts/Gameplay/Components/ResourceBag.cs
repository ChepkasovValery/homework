using System;
using System.Collections.Generic;
using Game.Scripts.SaveLoadSystem;
using Newtonsoft.Json;
using SampleGame.Common;
using UnityEngine;

namespace SampleGame.Gameplay
{
  //Can be extended
  public sealed class ResourceBag : MonoBehaviour, IComponentSerializer
  {
    ///Variable
    [field: SerializeField]
    public ResourceType Type { get; set; }

    ///Variable
    [field: SerializeField]
    public int Current { get; set; }

    ///Const
    [field: SerializeField]
    public int Capacity { get; set; }

    public void Serialize(IDictionary<string, string> state)
    {
      state[nameof(ResourceBag)] = JsonConvert.SerializeObject(
        new Dictionary<string, string>
        {
          { nameof(Type), Type.ToString() },
          { nameof(Current), Current.ToString() }
        });
    }

    public void Deserialize(IDictionary<string, string> state)
    {
      string json = state[nameof(ResourceBag)];

      var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

      if (data != null)
      {
        Type = Enum.Parse<ResourceType>(data[nameof(Type)]);
        Current = int.Parse(data[nameof(Current)]);
      }
    }
  }
}
using System;
using System.Collections.Generic;
using Game.Scripts.SaveLoadSystem;
using Newtonsoft.Json;
using SampleGame.Common;
using UnityEngine;

namespace SampleGame.Gameplay
{
  //Can be extended
  public sealed class Team : MonoBehaviour, IComponentSerializer
  {
    ///Variable
    [field: SerializeField]
    public TeamType Type { get; set; }

    public void Serialize(IDictionary<string, string> state)
    {
      state[nameof(Team)] = JsonConvert.SerializeObject(
        new Dictionary<string, string>
        {
          { nameof(Type), Type.ToString() }
        });
    }

    public void Deserialize(IDictionary<string, string> state)
    {
      string json = state[nameof(Team)];

      var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

      if (data != null) Type = Enum.Parse<TeamType>(data[nameof(Type)]);
    }
  }
}
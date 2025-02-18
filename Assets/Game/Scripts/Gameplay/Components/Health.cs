using System.Collections.Generic;
using Game.Scripts.SaveLoadSystem;
using Newtonsoft.Json;
using UnityEngine;

namespace SampleGame.Gameplay
{
  //Can be extended
  public sealed class Health : MonoBehaviour, IComponentSerializer
  {
    ///Variable
    [field: SerializeField]
    public int Current { get; set; } = 50;

    ///Const
    [field: SerializeField]
    public int Max { get; private set; } = 100;

    public void Serialize(IDictionary<string, string> state)
    {
      state[nameof(Health)] = JsonConvert.SerializeObject(
        new Dictionary<string, int>
        {
          { nameof(Current), Current }
        });
    }

    public void Deserialize(IDictionary<string, string> state)
    {
      string json = state[nameof(Health)];

      var data = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);

      if (data != null) Current = data[nameof(Current)];
    }
  }
}
using System.Collections.Generic;
using Game.Scripts.SaveLoadSystem;
using Newtonsoft.Json;
using UnityEngine;

namespace SampleGame.Gameplay
{
  //Can be extended
  public sealed class Countdown : MonoBehaviour, IComponentSerializer
  {
    ///Variable
    [field: SerializeField]
    public float Current { get; set; }

    ///Const
    [field: SerializeField]
    public float Duration { get; private set; }

    public void Serialize(IDictionary<string, string> state)
    {
      state[nameof(Countdown)] = JsonConvert.SerializeObject(new Dictionary<string, float>
      {
        { nameof(Current), Current }
      });
    }

    public void Deserialize(IDictionary<string, string> state)
    {
      string json = state[nameof(Countdown)];

      var data = JsonConvert.DeserializeObject<Dictionary<string, float>>(json);

      if (data != null) Current = data[nameof(Current)];
    }
  }
}
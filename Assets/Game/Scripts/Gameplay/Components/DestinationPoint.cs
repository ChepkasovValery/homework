using System.Collections.Generic;
using Game.Scripts.SaveLoadSystem;
using Modules.SaveLoadSystem;
using Newtonsoft.Json;
using UnityEngine;

namespace SampleGame.Gameplay
{
  //Can be extended
  public sealed class DestinationPoint : MonoBehaviour, IComponentSerializer
  {
    ///Variable
    [field: SerializeField]
    public Vector3 Value { get; set; }

    public void Serialize(IDictionary<string, string> state)
    {
      state[nameof(DestinationPoint)] = JsonConvert.SerializeObject(
        new Dictionary<string, SerializableVector3>
        {
          { nameof(Value), new SerializableVector3(Value) }
        });
    }

    public void Deserialize(IDictionary<string, string> state)
    {
      string json = state[nameof(DestinationPoint)];

      var data = JsonConvert.DeserializeObject<Dictionary<string, SerializableVector3>>(json);

      if (data != null) Value = data[nameof(Value)].ToVector3();
    }
  }
}
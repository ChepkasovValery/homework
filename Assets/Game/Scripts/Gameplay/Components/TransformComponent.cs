using System.Collections.Generic;
using Game.Scripts.SaveLoadSystem;
using Modules.SaveLoadSystem;
using Newtonsoft.Json;
using UnityEngine;

namespace SampleGame.Gameplay
{
  public class TransformComponent : MonoBehaviour, IComponentSerializer
  {
    public Vector3 Position => transform.position;
    public Vector3 Rotation => transform.rotation.eulerAngles;

    public void Serialize(IDictionary<string, string> state)
    {
      state[nameof(TransformComponent)] = JsonConvert.SerializeObject(
        new Dictionary<string, SerializableVector3>
        {
          { nameof(Position), new SerializableVector3(Position) },
          { nameof(Rotation), new SerializableVector3(Rotation) }
        });
    }

    public void Deserialize(IDictionary<string, string> state)
    {
      string json = state[nameof(TransformComponent)];

      var data = JsonConvert.DeserializeObject<Dictionary<string, SerializableVector3>>(json);

      if (data != null)
      {
        transform.position = data[nameof(Position)].ToVector3();
        transform.rotation = Quaternion.Euler(data[nameof(Rotation)].ToVector3());
      }
    }
  }
}
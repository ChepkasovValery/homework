using System.Collections.Generic;
using Game.Scripts.SaveLoadSystem;
using Modules.Entities;
using Newtonsoft.Json;
using UnityEngine;

namespace SampleGame.Gameplay
{
  //Can be extended
  public sealed class TargetObject : MonoBehaviour, IComponentSerializer
  {
    ///Variable
    [field: SerializeField]
    public Entity Value { get; set; }
      
    public void Serialize(IDictionary<string, string> state)
    {
      if (Value == null)
        return;

      state[nameof(TargetObject)] = JsonConvert.SerializeObject(
        new Dictionary<string, int>
        {
          { nameof(Value), Value.Id }
        });
    }

    public void Deserialize(IDictionary<string, string> state)
    {
      EntityWorld world = FindObjectOfType<EntityWorld>();
      
      if (state.TryGetValue(nameof(TargetObject), out string dataJson))
      {
        var data = JsonConvert.DeserializeObject<Dictionary<string, int>>(dataJson);

        if (data != null)
        {
          if (world.TryGet(data[nameof(Value)], out Entity entity))
          {
            Value = entity;
            
            return;
          }
        }
      }        
      
      Value = null;
    }
  }
}
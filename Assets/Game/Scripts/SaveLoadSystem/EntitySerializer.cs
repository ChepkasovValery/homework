using System.Collections.Generic;
using System.Linq;
using Modules.Entities;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.SaveLoadSystem
{
  public class EntitySerializer : IComponentSerializer
  {
    private readonly EntityWorld _entityWorld;

    public EntitySerializer(EntityWorld entityWorld)
    {
      _entityWorld = entityWorld;
    }

    public void Serialize(IDictionary<string, string> state)
    {
      IReadOnlyCollection<Entity> entities = _entityWorld.GetAll();
      Dictionary<string, string> entitiesData = new Dictionary<string, string>();

      foreach (Entity entity in entities)
      {
        Dictionary<string, string> entityData = new Dictionary<string, string>();
        
        entityData[nameof(entity.Name)] = entity.Name;

        foreach (IComponentSerializer componentSerializer in entity.GetComponents<IComponentSerializer>())
        {
          componentSerializer.Serialize(entityData);
        }
          
        entitiesData[entity.Id.ToString()] = JsonConvert.SerializeObject(entityData);
      }

      state[nameof(EntitySerializer)] = JsonConvert.SerializeObject(entitiesData);
    }

    public void Deserialize(IDictionary<string, string> state)
    {
      Dictionary<string, string> entitiesData = JsonConvert.DeserializeObject<Dictionary<string, string>>(state[nameof(EntitySerializer)]);

      RemoveUnsaved(entitiesData);
      
      SpawnEntities(entitiesData);
      
      DeserializeComponents(entitiesData);
    }

    private void DeserializeComponents(Dictionary<string, string> entitiesData)
    {
      foreach (KeyValuePair<string, string> keyValuePair in entitiesData)
      {
        int entityId = int.Parse(keyValuePair.Key);

        Dictionary<string, string> entityData = JsonConvert.DeserializeObject<Dictionary<string, string>>(keyValuePair.Value);

        if (_entityWorld.TryGet(entityId, out Entity entity))
        {
          DeserializeComponentsOnEntity(entity, entityData);
        }
      }
    }

    private void SpawnEntities(Dictionary<string, string> entitiesData)
    {
      foreach (KeyValuePair<string, string> keyValuePair in entitiesData)
      {
        int entityId = int.Parse(keyValuePair.Key);
        
        if(!_entityWorld.Has(entityId))
        {
          Dictionary<string, string> entityData = JsonConvert.DeserializeObject<Dictionary<string, string>>(keyValuePair.Value);

          _entityWorld.Spawn(entityData[nameof(Entity.Name)], Vector3.zero, Quaternion.identity, entityId);
        }
      }
    }
    
    private void RemoveUnsaved(Dictionary<string, string> entitiesData)
    {
      int[] allEntities = _entityWorld.GetAll().Select(entity => entity.Id).ToArray();
      
      foreach (int entityId in allEntities)
      {
        if (!entitiesData.ContainsKey(entityId.ToString()))
        {
          _entityWorld.Destroy(entityId);
        }
      }
    }

    private void DeserializeComponentsOnEntity(Entity entity, Dictionary<string, string> entityData)
    {
      foreach (IComponentSerializer componentSerializer in entity.GetComponents<IComponentSerializer>())
      {
        componentSerializer.Deserialize(entityData);
      }
    }
  }
}
using System.Collections.Generic;

namespace Game.Scripts.SaveLoadSystem
{
  public interface IComponentSerializer
  {
    void Serialize(IDictionary<string, string> state);
    void Deserialize(IDictionary<string, string> state);
  }
}
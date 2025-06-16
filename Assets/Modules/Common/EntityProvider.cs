using UnityEngine;

namespace Modules.Common
{
  public class EntityProvider : MonoBehaviour
  {
    public IEntity Entity => _entity;
    
    [SerializeField] private Entity _entity;
  }
}
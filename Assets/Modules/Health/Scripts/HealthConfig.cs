using UnityEngine;

namespace Modules.Health.Scripts
{
  [CreateAssetMenu(menuName = "Configs/Health config", fileName = "Health config")]
  public class HealthConfig : ScriptableObject
  {
    public float Health;
  }
}
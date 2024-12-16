using UnityEngine;

namespace Game.World
{
  public class World : MonoBehaviour, IWorld
  {
    public Transform Value => transform;
  }
}
using System;
using UnityEngine;

namespace Modules.SaveLoadSystem
{
  [Serializable]
  public class SerializableVector3
  {
    public float x;
    public float y;
    public float z;

    public SerializableVector3(Vector3 vector3)
    {
      x = vector3.x;
      y = vector3.y;
      z = vector3.z;
    }

    public Vector3 ToVector3()
    {
      return new Vector3(x, y, z);
    }
  }
}
using ShootEmUp;
using UnityEngine;

namespace Game.Gun
{
  [CreateAssetMenu(menuName = "Configs/Gun config", fileName = "gun config")]
  public class GunConfig : ScriptableObject
  {
    public int Damage;
    public float BulletSpeed;
    public Color BulletColor;
    public PhysicsLayer PhysicsLayer;
  }
}
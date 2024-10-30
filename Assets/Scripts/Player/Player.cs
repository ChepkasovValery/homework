using Game.Gun;
using Game.InputSystem;
using Game.Move;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
  [RequireComponent(typeof(Gun), typeof(MoveComponent))]
  public sealed class Player : MonoBehaviour
  {
    [SerializeField] private Gun _gun;

    public void Construct(BulletManager bulletManager)
    {
      _gun.Construct(bulletManager);
    }
  }
}
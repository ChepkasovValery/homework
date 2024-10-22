using Game.Gun;
using Game.InputSystem;
using Game.Move;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class Player : MonoBehaviour
  {
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private DirectionMoveComponent _directionMover;
    [SerializeField] private BulletsController _bulletsController;
    [SerializeField] private Gun _gun;

    private Vector3 _fireDirection = new Vector3(0, 1, 0);

    private void Awake()
    {
      _gun.Construct(_bulletsController);
    }

    private void OnEnable()
    {
      _playerInput.OnFirePressed += Fire;
    }

    private void OnDisable()
    {
      _playerInput.OnFirePressed -= Fire;
    }

    private void FixedUpdate()
    {
      _directionMover.Move(new Vector3(_playerInput.MoveDirection.x, 0, 0).normalized);
    }

    private void Fire()
    {
      _gun.Fire(_fireDirection);
    }
  }
}
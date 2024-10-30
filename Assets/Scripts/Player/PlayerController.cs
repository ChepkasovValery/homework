using Game.Gun;
using Game.InputSystem;
using Game.Move;
using UnityEngine;

namespace ShootEmUp
{
  public class PlayerController : MonoBehaviour
  {
    [SerializeField] private Player _player;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private BulletManager _bulletManager;
    [SerializeField] private Vector3 _fireDirection;

    private MoveComponent _moveComponent;
    private Gun _gun;

    private void Awake()
    {
      _player.Construct(_bulletManager);
      
      _moveComponent = _player.GetComponent<MoveComponent>();
      _gun = _player.GetComponent<Gun>();
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
      _moveComponent.Move(new Vector3(_playerInput.MoveDirection.x, 0, 0).normalized);
    }

    private void Fire()
    {
      _gun.Fire(_fireDirection);
    }
  }
}
using System;
using Game.Modules.GroundChecker.Scripts;
using Game.Modules.Input.Scripts;
using Game.Modules.Moving.Scripts;
using Modules.Health.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Modules.Character.Scripts
{
  public class CharacterMoveController : ITickable, IInitializable, IDisposable
  {
    private readonly IMover _mover;
    private readonly IInput _input;
    private readonly IHealth _health;
    private readonly IGroundChecker _groundChecker;
    private readonly IJumper _jumper;
    private readonly IBodyRotator _bodyRotator;
    
    public CharacterMoveController(IMover mover, IInput input, IGroundChecker groundChecker, IHealth health, IJumper jumper, IBodyRotator bodyRotator)
    {
      _mover = mover;
      _input = input;
      _groundChecker = groundChecker;
      _health = health;
      _jumper = jumper;
      _bodyRotator = bodyRotator;
    }

    public void Initialize()
    {
      _input.OnJumpPressed += TryJump;
    }

    public void Dispose()
    {
      _input.OnJumpPressed -= TryJump;
    }

    public void Tick()
    {
      if (_health.IsAlive && !Mathf.Approximately(_input.MoveDirection.x, 0))
      {
        _mover.Move(_input.MoveDirection);
        _bodyRotator.LookToDirection(_input.MoveDirection);
      }
    }

    private void TryJump()
    {
      if(_groundChecker.IsGrounded() && _health.IsAlive)
        _jumper.Jump();
    }
  }
}
using System;
using Game.Scripts.Input;
using Game.Scripts.Moving;
using Zenject;

namespace Game.Scripts.Character
{
  public class CharacterMoveController : ITickable, IInitializable, IDisposable
  {
    private readonly IMover _mover;
    private readonly IInput _input;

    public CharacterMoveController(IMover mover, IInput input)
    {
      _mover = mover;
      _input = input;
    }

    public void Initialize()
    {
      _input.OnJumpPressed += Jump;
    }

    public void Dispose()
    {
      _input.OnJumpPressed -= Jump;
    }

    public void Tick()
    {
      _mover.Move(_input.MoveDirection);
    }

    private void Jump()
    {
      _mover.Jump();
    }
  }
}
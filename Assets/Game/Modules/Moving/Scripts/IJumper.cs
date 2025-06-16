using System;

namespace Game.Modules.Moving.Scripts
{
  public interface IJumper
  {
    event Action OnJumped;
    void Jump();
  }
}
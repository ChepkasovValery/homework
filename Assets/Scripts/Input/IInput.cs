using System;
using UnityEngine;

namespace Modules
{
  public interface IInput
  {
    event Action<SnakeDirection> OnInputDirection;
  }
}
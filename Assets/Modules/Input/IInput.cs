using System;
using UnityEngine;

namespace Modules
{
  public interface IInput
  {
    event Action<Vector2Int> OnInputDirection;
  }
}
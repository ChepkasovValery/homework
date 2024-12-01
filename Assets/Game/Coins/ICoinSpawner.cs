using System;
using Modules;
using UnityEngine;

namespace Game.Coins
{
  public interface ICoinSpawner
  {
    event Action<ICoin> OnCoinSpawned;
    ICoin Spawn(Vector2Int position);
  }
}
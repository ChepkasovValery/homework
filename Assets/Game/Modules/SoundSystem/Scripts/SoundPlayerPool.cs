using UnityEngine;
using Zenject;

namespace Game.Modules.SoundSystem.Scripts
{
  public class SoundPlayerPool : MonoMemoryPool<Vector3, SoundPlayer>
  {
    protected override void Reinitialize(Vector3 p1, SoundPlayer item)
    {
      item.transform.position = p1;

      item.OnComplete += ReturnToPool;
    }

    private void ReturnToPool(SoundPlayer soundPlayer)
    {
      soundPlayer.OnComplete -= ReturnToPool;
      
      Despawn(soundPlayer);
    }
  }
}
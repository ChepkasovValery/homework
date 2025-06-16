using System;
using System.Collections;
using UnityEngine;

namespace Game.Modules.SoundSystem.Scripts
{
  public class SoundPlayer : MonoBehaviour
  {
    public event Action<SoundPlayer> OnComplete;
    
    [SerializeField] private AudioSource _src;
    
    public void PlayOneShot(AudioClip clip)
    {
      _src.PlayOneShot(clip);

      StartCoroutine(WaitForComplete(clip.length));
    }

    private IEnumerator WaitForComplete(float duration)
    {
      yield return new WaitForSeconds(duration);
      
      OnComplete?.Invoke(this);
    }
  }
}
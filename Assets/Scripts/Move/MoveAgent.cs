using System;
using System.Collections;
using Game.Configs;
using UnityEngine;

namespace Game.Move
{
  public class MoveAgent : MonoBehaviour
  {
    [SerializeField] private MoveComponent _moveComponent;
    [SerializeField] private float _stopDistance;
    
    public void MoveTo(Vector2 destination, Action onReachedCallback)
    {
      Stop();

      StartCoroutine(MoveToCor(destination, onReachedCallback));
    }

    public void Stop()
    {
      StopAllCoroutines();
    }

    private IEnumerator MoveToCor(Vector2 destination, Action onReachedCallback)
    {
      Vector2 moveVector;
      
      do
      {
        moveVector = destination - (Vector2)transform.position;
        _moveComponent.Move(moveVector.normalized);

        yield return new WaitForFixedUpdate();
      } 
      while (moveVector.magnitude > _stopDistance);
      
      onReachedCallback?.Invoke();
    }
  }
}
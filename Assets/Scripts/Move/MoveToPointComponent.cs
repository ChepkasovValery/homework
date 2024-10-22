using System;
using System.Collections;
using Game.Configs;
using UnityEngine;

namespace Game.Move
{
  public class MoveToPointComponent : MonoBehaviour
  {
    [SerializeField] private MoveConfig _moveConfig;
    [SerializeField] private Rigidbody2D _rigidbody;
    
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
        Vector2 direction = moveVector.normalized * Time.fixedDeltaTime;
        Vector2 nextPosition = _rigidbody.position + direction * _moveConfig.Speed;
        _rigidbody.MovePosition(nextPosition);

        yield return new WaitForFixedUpdate();
      } 
      while (moveVector.magnitude > 0.25f);
      
      onReachedCallback?.Invoke();
    }
  }
}
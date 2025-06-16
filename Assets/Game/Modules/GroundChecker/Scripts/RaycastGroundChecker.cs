using UnityEngine;

namespace Game.Modules.GroundChecker.Scripts
{
  public class RaycastGroundChecker : IGroundChecker
  {
    private const string GROUND_LAYER_NAME = "Ground";

    private readonly Transform _origin;
    private readonly float _checkDistance;

    public RaycastGroundChecker(Transform origin, float checkDistance)
    {
      _origin = origin;
      _checkDistance = checkDistance;
    }

    public bool IsGrounded()
    {
      RaycastHit2D hit = Physics2D.Raycast(_origin.position, Vector2.down, _checkDistance, LayerMask.GetMask(GROUND_LAYER_NAME));

      if (hit.collider != null)
      {
        return true;
      }

      return false;
    }
  }
}
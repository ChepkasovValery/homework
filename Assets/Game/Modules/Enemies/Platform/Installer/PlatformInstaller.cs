using Game.Modules.Moving.Configs;
using Game.Modules.Moving.Scripts;
using Game.Modules.Patrolling.Scripts;
using Modules.Common;
using UnityEngine;
using Zenject;

namespace Game.Modules.Enemies.Platform.Installer
{
  public class PlatformInstaller : MonoInstaller
  {
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private MoveConfig _moveConfig;
    [SerializeField] private Transform[] _waypoints;

    public override void InstallBindings()
    {
      Container.BindInterfacesAndSelfTo<Entity>().FromComponentOnRoot().AsSingle();
      Container.Bind<GameObject>().FromInstance(gameObject).AsSingle();
      Container.Bind<Transform>().FromInstance(transform).AsSingle();
      Container.Bind<Rigidbody2D>().FromInstance(_rigidbody2D).AsSingle();
      
      Container.BindInterfacesTo<RigidbodyVelocityMover>().AsSingle().WithArguments(_moveConfig);
      
      Container.BindInterfacesTo<Patrolling.Scripts.Patrolling>().AsSingle().WithArguments(GetWaypoints());
    }
    
    private Vector3[] GetWaypoints()
    {
      Vector3[] waypoints = new Vector3[_waypoints.Length];

      for (int i = 0; i < _waypoints.Length; i++)
      {
        waypoints[i] = _waypoints[i].position;
      }
      
      return waypoints;
    }
  }
}
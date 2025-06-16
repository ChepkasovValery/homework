using Game.Modules.Animations.Damage.Configs;
using Game.Modules.Animations.Damage.Scripts;
using Game.Modules.Attacks.Configs;
using Game.Modules.Attacks.Controllers;
using Game.Modules.Attacks.Push.Scripts;
using Game.Modules.Damage.Scripts;
using Game.Modules.Enemies.Controllers;
using Game.Modules.Enemies.Snake.Scripts;
using Game.Modules.Moving.Configs;
using Game.Modules.Moving.Scripts;
using Game.Modules.Trigger.Scripts;
using Modules.Common;
using Modules.Health.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Modules.Enemies.Snake.Installer
{
  public class SnakeInstaller : MonoInstaller
  {
    [SerializeField] private HealthConfig _healthConfig;
    [SerializeField] private DamageConfig _damageConfig;
    [SerializeField] private AttackConfig _attackConfig;
    [SerializeField] private MoveConfig _moveConfig;
    [SerializeField] private DamageAnimationConfig _damageAnimationConfig;
    [SerializeField] private TriggerWatcher2D _triggerWatcher2D;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _body;

    public override void InstallBindings()
    {
      Container.BindInterfacesAndSelfTo<Entity>().FromComponentOnRoot().AsSingle();
      Container.Bind<GameObject>().FromInstance(gameObject).AsSingle();
      Container.Bind<Transform>().FromInstance(transform).AsSingle();
      Container.Bind<Rigidbody2D>().FromInstance(_rigidbody2D).AsSingle();
      Container.Bind<SpriteRenderer>().FromInstance(_spriteRenderer).AsSingle();
      
      Container.BindInterfacesTo<Health>().AsSingle().WithArguments(_healthConfig);
      Container.BindInterfacesTo<DamageAnimation>().AsSingle().WithArguments(_damageAnimationConfig);
      Container.BindInterfacesTo<DeathObserver>().AsSingle().NonLazy();

      Container.BindInterfacesTo<DamageDealer>().AsSingle().WithArguments(_damageConfig);

      Container.BindInterfacesTo<TriggerWatcher2D>().FromInstance(_triggerWatcher2D).AsSingle();

      Container.BindInterfacesTo<PushAttack>().AsSingle().WithArguments(_attackConfig);
      Container.BindInterfacesTo<AttackController>().AsSingle();

      Container.BindInterfacesTo<RigidbodyForceMover>().AsSingle().WithArguments(_moveConfig);
      Container.BindInterfacesTo<Patrolling.Scripts.Patrolling>().AsSingle().WithArguments(GetWaypoints());
      Container.BindInterfacesTo<PatrollingController>().AsSingle();
      Container.BindInterfacesTo<BodyRotator>().AsSingle().WithArguments(_body);
      Container.BindInterfacesTo<SnakeBodyRotator>().AsSingle();
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
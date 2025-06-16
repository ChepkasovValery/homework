using Game.Modules.Animations.Damage.Configs;
using Game.Modules.Animations.Damage.Scripts;
using Game.Modules.Attacks.Configs;
using Game.Modules.Attacks.Controllers;
using Game.Modules.Attacks.Toss.Scripts;
using Game.Modules.Damage.Scripts;
using Game.Modules.Enemies.Controllers;
using Game.Modules.Moving.Configs;
using Game.Modules.Moving.Scripts;
using Game.Modules.Patrolling.Scripts;
using Game.Modules.Trigger.Scripts;
using Modules.Common;
using Modules.Health.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Modules.Enemies.Spider.Installer
{
  public class SpiderInstaller : MonoInstaller
  {
    [SerializeField] private HealthConfig _healthConfig;
    [SerializeField] private DamageConfig _damageConfig;
    [SerializeField] private AttackConfig _attackConfig;
    [SerializeField] private DamageAnimationConfig _damageAnimationConfig;
    [SerializeField] private MoveConfig _moveConfig;
    [SerializeField] private TriggerWatcher2D _triggerWatcher;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    public override void InstallBindings()
    {
      Container.BindInterfacesAndSelfTo<Entity>().FromComponentOnRoot().AsSingle();
      Container.Bind<GameObject>().FromInstance(gameObject).AsSingle();
      Container.Bind<Transform>().FromInstance(transform).AsSingle();
      Container.Bind<Rigidbody2D>().FromInstance(_rigidbody2D).AsSingle();
      Container.Bind<SpriteRenderer>().FromInstance(_spriteRenderer).AsSingle();

      Container.BindInterfacesTo<Health>().AsSingle().WithArguments(_healthConfig);
      Container.BindInterfacesTo<DeathObserver>().AsSingle().NonLazy();
      
      Container.BindInterfacesTo<DamageDealer>().AsSingle().WithArguments(_damageConfig);

      Container.BindInterfacesTo<TriggerWatcher2D>().FromInstance(_triggerWatcher).AsSingle();

      Container.BindInterfacesTo<TossAttack>().AsSingle().WithArguments(_attackConfig);
      Container.BindInterfacesTo<AttackController>().AsSingle();

      Container.BindInterfacesTo<DamageAnimation>().AsSingle().WithArguments(_damageAnimationConfig);

      Container.BindInterfacesTo<RigidbodyForceMover>().AsSingle().WithArguments(_moveConfig);
      Container.BindInterfacesTo<Patrolling.Scripts.Patrolling>().AsSingle().WithArguments(GetWaypoints());
      Container.BindInterfacesTo<PatrollingController>().AsSingle();
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
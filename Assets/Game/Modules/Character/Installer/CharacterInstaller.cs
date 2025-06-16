using Game.Modules.Animations.Damage.Configs;
using Game.Modules.Animations.Damage.Scripts;
using Game.Modules.Attacks.Configs;
using Game.Modules.Attacks.Push.Scripts;
using Game.Modules.Attacks.Toss.Scripts;
using Game.Modules.Character.Animations;
using Game.Modules.Character.Animations.Configs;
using Game.Modules.Character.Scripts;
using Game.Modules.Cooldown.Configs;
using Game.Modules.Cooldown.Scripts;
using Game.Modules.Damage.Scripts;
using Game.Modules.GroundChecker.Scripts;
using Game.Modules.Moving.Configs;
using Game.Modules.Moving.Scripts;
using Game.Modules.Trigger.Scripts;
using Modules.Common;
using Modules.Health.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Modules.Character.Installer
{
  public class CharacterInstaller : MonoInstaller
  {
    [Header("Moving")]
    [SerializeField] private MoveConfig _moveConfig;
    [SerializeField] private JumpConfig _jumpConfig;
    [SerializeField] private float _groundCheckDistance;

    [Header("Health")]
    [SerializeField] private HealthConfig _healthConfig;
    
    [Header("Animations")]
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private CharacterAnimationsConfig _characterAnimationsConfig;
    [SerializeField] private DamageAnimationConfig _damageAnimationConfig;
    [SerializeField] private Transform _targetForAnimation;
    
    [Header("Attack")]
    [SerializeField] private TriggerWatcher2D _attackTrigger;
    [SerializeField] private AttackConfig _tossAttackConfig;
    [SerializeField] private CooldownConfig _tossCooldownConfig;
    [SerializeField] private AttackConfig _pushAttackConfig;
    [SerializeField] private CooldownConfig _pushCooldownConfig;
    
    public override void InstallBindings()
    {
      BindUnityComponents();

      Container.BindInterfacesTo<Health>().AsSingle().WithArguments(_healthConfig);

      Container.BindInterfacesTo<RaycastGroundChecker>().AsSingle().WithArguments(transform, _groundCheckDistance);
      
      Container.BindInterfacesTo<RigidbodyForceMover>().AsSingle().WithArguments(_moveConfig);
      Container.BindInterfacesTo<BodyRotator>().AsSingle().WithArguments(transform);
      Container.BindInterfacesTo<RigidbodyJumper>().AsSingle().WithArguments(_jumpConfig);
      
      Container.BindInterfacesTo<CharacterMoveController>().AsSingle().NonLazy();

      Container.BindInterfacesTo<CharacterAnimator>().AsSingle().WithArguments(_characterAnimationsConfig, _targetForAnimation).NonLazy();
      Container.BindInterfacesTo<DamageAnimation>().AsSingle().WithArguments(_damageAnimationConfig);

      Container.BindInterfacesAndSelfTo<CharacterSoundController>().AsSingle().NonLazy();
      
      Container.BindInterfacesTo<DeathObserver>().AsSingle().NonLazy();
      
      BindAttacks();
    }

    private void BindAttacks()
    {
      Container.BindInterfacesAndSelfTo<PushAttack>().AsSingle().WithArguments(_pushAttackConfig);
      Container.BindInterfacesAndSelfTo<TossAttack>().AsSingle().WithArguments(_tossAttackConfig);
      
      Container.BindInterfacesTo<CharacterTossAttackController>().AsSingle().WithArguments(_attackTrigger, new Cooldown.Scripts.Cooldown(_tossCooldownConfig));
      Container.BindInterfacesTo<CharacterPushAttackController>().AsSingle().WithArguments(_attackTrigger, new Cooldown.Scripts.Cooldown(_pushCooldownConfig));
    }

    private void BindUnityComponents()
    {
      Container.BindInterfacesAndSelfTo<Entity>().FromComponentOnRoot().AsSingle();
      Container.Bind<Rigidbody2D>().FromComponentInHierarchy().AsSingle();
      Container.Bind<Transform>().FromInstance(transform).AsSingle();
      Container.Bind<GameObject>().FromInstance(gameObject).AsSingle();
      Container.Bind<SpriteRenderer>().FromInstance(_renderer).AsSingle();
    }
  }
}
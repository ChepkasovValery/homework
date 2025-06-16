using Game.Modules.CollisionWatcher.Scripts;
using Game.Modules.Damage.Scripts;
using Game.Modules.Enemies.Trap.Scripts;
using Modules.Common;
using Modules.Health.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Modules.Enemies.Trap.Installer
{
  public class TrapInstaller : MonoInstaller
  {
    [SerializeField] private HealthConfig _healthConfig;
    [SerializeField] private DamageConfig _damageConfig;
    
    public override void InstallBindings()
    {
      Container.BindInterfacesAndSelfTo<Entity>().FromComponentOnRoot().AsSingle();
      Container.Bind<GameObject>().FromInstance(gameObject).AsSingle();
      Container.Bind<Rigidbody2D>().FromComponentOnRoot().AsSingle();

      Container.BindInterfacesTo<Health>().AsSingle().WithArguments(_healthConfig);

      Container.BindInterfacesTo<DamageDealer>().AsSingle().WithArguments(_damageConfig);
      
      Container.BindInterfacesTo<CollisionWatcher2D>().FromNewComponentOnRoot().AsSingle();

      Container.BindInterfacesTo<TrapAttackController>().AsSingle().NonLazy();
      Container.BindInterfacesTo<TrapHealthController>().AsSingle().NonLazy();
    }
  }
}
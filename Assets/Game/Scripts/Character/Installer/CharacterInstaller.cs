using Game.Scripts.Moving;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Character.Installer
{
  public class CharacterInstaller : MonoInstaller
  {
    [SerializeField] private MoveConfig _moveConfig;
    
    public override void InstallBindings()
    {
      Container.Bind<Rigidbody2D>().FromComponentInHierarchy().AsSingle();
      Container.BindInterfacesTo<RigidbodyMover>().AsSingle().WithArguments(_moveConfig);
      Container.BindInterfacesTo<BodyRotator>().AsSingle().WithArguments(transform);
      
      Container.BindInterfacesTo<CharacterMoveController>().AsSingle().NonLazy();
    }
  }
}
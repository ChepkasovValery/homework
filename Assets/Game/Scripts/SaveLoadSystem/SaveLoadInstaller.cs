using Modules.SaveLoadSystem;
using UnityEngine;
using Zenject;

namespace Game.Scripts.SaveLoadSystem
{
  [CreateAssetMenu(
    fileName = "SaveLoadInstaller",
    menuName = "Zenject/New Save Load Installer"
  )]
  public class SaveLoadInstaller : ScriptableObjectInstaller
  {
    [SerializeField] private string _uri;

    public override void InstallBindings()
    {
      this.Container.BindInterfacesAndSelfTo<GameSaveLoader>().AsSingle();
      this.Container.BindInterfacesAndSelfTo<NetworkGameRepository>().AsSingle().WithArguments(_uri);
      this.Container.BindInterfacesAndSelfTo<EntitySerializer>().AsSingle();
    }
  }
}
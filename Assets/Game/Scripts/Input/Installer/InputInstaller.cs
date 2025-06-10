using UnityEngine;
using Zenject;

namespace Game.Scripts.Input.Installer
{
  [CreateAssetMenu(menuName = "Zenject/Installers/InputInstaller", fileName = "Input installer")]
  public class InputInstaller : ScriptableObjectInstaller
  {
    public override void InstallBindings()
    {
      Container.BindInterfacesTo<KeyboardInput>().AsSingle();
    }
  }
}
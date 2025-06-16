using UnityEngine;
using Zenject;

namespace Game.Modules.SoundSystem.Scripts
{
  [CreateAssetMenu(menuName = "Zenject/Installers/Sound system", fileName = "SoundSystemInstaller")]
  public class SoundSystemInstaller : ScriptableObjectInstaller
  {
    [SerializeField] private SoundPlayer _soundPlayerPrefab;
    [SerializeField] private SoundsConfig _soundsConfig;
    
    public override void InstallBindings()
    {
      Container.BindMemoryPool<SoundPlayer, SoundPlayerPool>()
        .FromComponentInNewPrefab(_soundPlayerPrefab)
        .UnderTransformGroup("Sound players");

      Container.BindInterfacesTo<SoundManager>().AsSingle().WithArguments(_soundsConfig);
    }
  }
}
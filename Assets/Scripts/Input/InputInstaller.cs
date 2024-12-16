using Zenject;

namespace Modules
{
  public class InputInstaller : Installer<InputInstaller>
  {
    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<Input>()
        .AsSingle();
    }
  }
}
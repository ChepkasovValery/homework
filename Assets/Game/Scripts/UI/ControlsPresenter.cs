using System;
using Game.Scripts.SaveLoadSystem;

namespace Game.Gameplay
{
  public sealed class ControlsPresenter : IControlsPresenter
  {
    private readonly GameSaveLoader _gameSaveLoader;

    public ControlsPresenter(GameSaveLoader gameSaveLoader)
    {
      _gameSaveLoader = gameSaveLoader;
    }

    public void Save(Action<bool, int> callback)
    {
      _gameSaveLoader.Save(callback).Forget();
    }

    public void Load(string versionText, Action<bool, int> callback)
    {
      _gameSaveLoader.Load(int.Parse(versionText), callback).Forget();
    }
  }
}
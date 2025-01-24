using Modules.Planets;

namespace Game.Views
{
  public interface IPlanetPopupPresenter
  {
    string Title { get; }
    string Population { get; }
    string Level { get; }
    string Income { get; }
    string UpgradeCost { get; }
    bool IsCanUpgrade { get; }
    bool IsMaxLevel { get; }
    void SetPlanet(IPlanet planet);
    bool Upgrade();
  }
}
using Game.Views;
using Modules.Planets;

namespace Game.Presenters
{
  public class PlanetPopupPresenter : IPlanetPopupPresenter
  {
    public string Title => _planet.Name;
    public string Population => $"Population: {_planet.Population}";
    public string Level => $"Level: {_planet.Level} / {_planet.MaxLevel}";
    public string Income => $"Income: {_planet.MinuteIncome} / sec";
    public string UpgradeCost => _planet.Price.ToString();
    public bool IsCanUpgrade => _planet.CanUpgrade;
    public bool IsMaxLevel => _planet.IsMaxLevel;

    private IPlanet _planet;

    public void SetPlanet(IPlanet planet)
    {
      _planet = planet;
    }

    public bool Upgrade()
    {
      if (_planet.CanUpgrade)
      {
        return _planet.Upgrade();
      }

      return false;
    }
  }
}
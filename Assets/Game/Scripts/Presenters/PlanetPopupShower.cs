using Game.Views;
using Modules.Planets;

namespace Game.Scripts
{
  public class PlanetPopupShower
  {
    private readonly IPlanetPopupPresenter _planetPopupPresenter;
    private readonly PlanetPopup _planetPopup;

    public PlanetPopupShower(IPlanetPopupPresenter planetPopupPresenter, PlanetPopup planetPopup)
    {
      _planetPopup = planetPopup;
      _planetPopupPresenter = planetPopupPresenter;
    }

    public void Show(IPlanet planet)
    {
      _planetPopupPresenter.SetPlanet(planet);
      _planetPopup.Show(_planetPopupPresenter);
    }
  }
}
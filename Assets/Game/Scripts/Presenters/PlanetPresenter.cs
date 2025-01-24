using System;
using Game.Scripts;
using Game.Views;
using Modules.Planets;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
  public class PlanetPresenter : IInitializable, IDisposable
  {
    public event Action<PlanetPresenter> OnIncomeGathered;
    public Vector3 CoinIconPosition => _planetView.CoinIconPosition;
    
    private readonly IPlanet _planet;
    private readonly PlanetView _planetView;
    private readonly PlanetPopupShower _planetPopupShower;

    public PlanetPresenter(IPlanet planet, PlanetView planetView, PlanetPopupShower planetPopupShower)
    {
      _planetPopupShower = planetPopupShower;
      _planetView = planetView;
      _planet = planet;
    }

    public void Initialize()
    {
      _planet.OnIncomeTimeChanged += UpdateIncomeTime;
      _planet.OnIncomeReady += IncomeReady;
      _planet.OnUnlocked += UpdateView;
      _planetView.OnClick += Clicked;
      _planetView.OnHold += ShowPopup;
      
      UpdateView();
    }

    public void Dispose()
    {
      _planet.OnIncomeTimeChanged -= UpdateIncomeTime;
      _planet.OnIncomeReady -= IncomeReady;
      _planet.OnUnlocked -= UpdateView;
      _planetView.OnClick -= Clicked;
      _planetView.OnHold -= ShowPopup;
    }

    private void UpdateView()
    {
      _planetView.SetIcon(_planet.GetIcon(_planet.IsUnlocked));
      _planetView.SetLocked(!_planet.IsUnlocked);
      _planetView.SetPrice(_planet.Price.ToString());
      _planetView.SetPriceActive(!_planet.IsUnlocked);
      _planetView.SetProgress(_planet.IncomeProgress);
      _planetView.SetActiveCoin(_planet.IsIncomeReady);
    }

    private void UpdateIncomeTime(float time)
    {
      _planetView.SetProgressTime($"{(int)(time / 60)}m:{(int)(time % 60)}s");
      _planetView.SetProgress(_planet.IncomeProgress);
    }

    private void Clicked()
    {
      if (_planet.IsUnlocked)
      {
        if (_planet.IsIncomeReady)
        {
          if (_planet.GatherIncome())
          {
            UpdateView();
            OnIncomeGathered?.Invoke(this);
          }
        }
      }
      else
      {
        _planet.Unlock();
      }
    }

    private void ShowPopup()
    {
      _planetPopupShower.Show(_planet);
    }

    private void IncomeReady(bool ready)
    {
      _planetView.SetActiveCoin(true);
      _planetView.SetActiveProgress(false);
    }
  }
}
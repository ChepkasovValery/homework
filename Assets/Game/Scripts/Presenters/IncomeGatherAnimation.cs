using System;
using Game.Presenters;
using Modules.UI;
using Zenject;

namespace Game.Scripts.Presenters
{
  public class IncomeGatherAnimation : IInitializable, IDisposable
  {
    private readonly MoneyPresenter _moneyPresenter;
    private readonly ParticleAnimator _particleAnimator;
    private readonly PlanetPresenter[] _planetPresenters;

    public IncomeGatherAnimation(PlanetPresenter[] planetPresenters, MoneyPresenter moneyPresenter, ParticleAnimator particleAnimator)
    {
      _planetPresenters = planetPresenters;
      _moneyPresenter = moneyPresenter;
      _particleAnimator = particleAnimator;
    }

    public void Initialize()
    {
      foreach (PlanetPresenter planetPresenter in _planetPresenters)
      {
        planetPresenter.OnIncomeGathered += Show;
      }
    }

    public void Dispose()
    {
      foreach (PlanetPresenter planetPresenter in _planetPresenters)
      {
        planetPresenter.OnIncomeGathered -= Show;
      }
    }

    public void Show(PlanetPresenter planetPresenter)
    {
      _particleAnimator.Emit(
        planetPresenter.CoinIconPosition, 
        _moneyPresenter.CoinIconPosition, 
        1f, 
        () =>
      {
        _moneyPresenter.UpdateViewWithAnim();
      });
    }
  }
}
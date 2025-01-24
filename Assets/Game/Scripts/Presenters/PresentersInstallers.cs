using Game.Scripts;
using Game.Scripts.Presenters;
using Game.Views;
using Modules.Planets;
using Modules.UI;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    [CreateAssetMenu(
        fileName = "PresentersInstallers",
        menuName = "Zenject/New PresentersInstallers"
    )]
    public sealed class PresentersInstallers : MonoInstaller
    {
        [Inject] private IPlanet[] _planets;
        [Inject] private PlanetView[] _planetViews;
        [Inject] private MoneyView _moneyView;
        [Inject] private PlanetPopup _planetPopup;

        private ParticleAnimator _particleAnimator;
        
        public override void InstallBindings()
        {
            BindPlanetPopup();
            BindPlanetPresenters();
            BindMoneyPresenter();
            BindAnimations();
        }

        private void BindAnimations()
        {
            Container.BindInterfacesAndSelfTo<ParticleAnimator>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<IncomeGatherAnimation>().AsSingle().NonLazy();
        }

        private void BindPlanetPopup()
        {
            Container.BindInterfacesAndSelfTo<PlanetPopupShower>().AsSingle().WithArguments(_planetPopup);
            Container.BindInterfacesAndSelfTo<PlanetPopupPresenter>().AsSingle();
        }

        private void BindMoneyPresenter()
        {
            Container.BindInterfacesAndSelfTo<MoneyPresenter>().AsSingle().WithArguments(_moneyView).NonLazy();
        }

        private void BindPlanetPresenters()
        {
            for (int i = 0; i < _planets.Length; i++)
            {
                Container.BindInterfacesAndSelfTo<PlanetPresenter>().AsCached().WithArguments(_planets[i], _planetViews[i]).NonLazy();
            }
        }
    }
}
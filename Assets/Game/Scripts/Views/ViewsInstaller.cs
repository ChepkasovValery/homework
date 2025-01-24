using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class ViewsInstaller : MonoInstaller
    {
        [SerializeField] private PlanetView[] _planetViews;
        [SerializeField] private MoneyView _moneyView;
        [SerializeField] private PlanetPopup _planetPopup;
        
        public override void InstallBindings()
        {
            BindPlanetPopup();
            BindPlanetViews();
            BindMoneyView();
        }

        private void BindPlanetPopup()
        {
            Container.BindInterfacesAndSelfTo<PlanetPopup>().FromInstance(_planetPopup).AsSingle();
        }

        private void BindMoneyView()
        {
            Container.BindInterfacesAndSelfTo<MoneyView>().FromInstance(_moneyView).AsSingle();
        }

        private void BindPlanetViews()
        {
            foreach (PlanetView planetView in _planetViews)
            {
                Container.BindInterfacesAndSelfTo<PlanetView>().FromInstance(planetView).AsCached().NonLazy();
            }
        }
    }
}
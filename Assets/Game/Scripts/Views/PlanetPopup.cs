using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
  public class PlanetPopup : MonoBehaviour
  {
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _population;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _income;
    [SerializeField] private TextMeshProUGUI _upgradeCost;
    [SerializeField] private GameObject _maxLevelText;
    [SerializeField] private GameObject _priceText;
    [SerializeField] private GameObject _upgradeText;
    
    private IPlanetPopupPresenter _planetPopupPresenter;
    
    public void Show(IPlanetPopupPresenter planetPopupPresenter)
    {
      _planetPopupPresenter = planetPopupPresenter;

      gameObject.SetActive(true);
      
      UpdateView();
    }

    private void OnEnable()
    {
      _closeButton.onClick.AddListener(Close);
      _upgradeButton.onClick.AddListener(Upgrade);
    }

    private void OnDisable()
    {
      _closeButton.onClick.RemoveListener(Close);
      _upgradeButton.onClick.RemoveListener(Upgrade);
    }

    private void UpdateView()
    {
      UpdateTitle();
      UpdatePopulation();
      UpdateLevel();
      UpdateIncome();
      UpdateUpgradeButton();
    }

    private void UpdateTitle() => _title.text = _planetPopupPresenter.Title;

    private void UpdatePopulation() => _population.text = _planetPopupPresenter.Population;

    private void UpdateLevel() => _level.text = _planetPopupPresenter.Level;

    private void UpdateIncome() => _income.text = _planetPopupPresenter.Income;

    private void UpdateUpgradeButton()
    {
      _maxLevelText.SetActive(_planetPopupPresenter.IsMaxLevel);
      _upgradeText.SetActive(!_planetPopupPresenter.IsMaxLevel);
      _priceText.SetActive(!_planetPopupPresenter.IsMaxLevel);
      _upgradeButton.interactable = _planetPopupPresenter.IsCanUpgrade;
        
      _upgradeCost.text = _planetPopupPresenter.UpgradeCost;
    }

    private void Upgrade()
    {
      if (_planetPopupPresenter.Upgrade())
      {
        UpdateView();
      }
    }

    private void Close()
    {
      gameObject.SetActive(false);
    }
  }
}
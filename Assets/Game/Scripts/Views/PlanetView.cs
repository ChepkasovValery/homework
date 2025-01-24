using Modules.UI;
using TMPro;
using UnityEngine;
using Action = Unity.Plastic.Antlr3.Runtime.Misc.Action;
using Image = UnityEngine.UI.Image;

namespace Game.Views
{
  public class PlanetView : MonoBehaviour
  {
    public Vector3 CoinIconPosition => _coin.transform.position;
    
    public event Action OnClick;
    public event Action OnHold;
    
    [SerializeField] private SmartButton _smartButton;
    [SerializeField] private GameObject _lock;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private TextMeshProUGUI _progressTime;
    [SerializeField] private Image _progress;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _income;
    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject _priceObj;

    private void OnEnable()
    {
      _smartButton.OnClick += Click;
      _smartButton.OnHold += Hold;
    }
    
    private void OnDisable()
    {
      _smartButton.OnClick -= Click;
      _smartButton.OnHold -= Hold;
    }

    public void SetIcon(Sprite icon)
    {
      _icon.sprite = icon;
    }

    public void SetActiveCoin(bool active)
    {
      _coin.SetActive(active);
    }

    public void SetActiveProgress(bool active)
    {
      _income.SetActive(active);
    }
    
    public void SetLocked(bool locked)
    {
      _lock.SetActive(locked);
      SetActiveProgress(!locked);
      SetActiveCoin(!locked);
    }

    public void SetPrice(string price)
    {
      _price.text = price;
    }

    public void SetPriceActive(bool active)
    {
      _priceObj.SetActive(active);
    }

    public void SetProgress(float progress)
    {
      _progress.fillAmount = progress;
    }

    public void SetProgressTime(string time)
    {
      _progressTime.text = time;
    }

    private void Hold()
    {
      OnHold?.Invoke();
    }

    private void Click()
    {
      OnClick?.Invoke();
    }
  }
}
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Views
{
  public class MoneyView : MonoBehaviour
  {
    public Vector3 IconPosition => _icon.position;
    
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _animationDuration;
    [SerializeField] private Transform _icon;
    
    public void UpdateMoney(string value)
    {
      _text.text = value;
    }

    public void UpdateMoneyWithAnim(int from, int to)
    {
      DOVirtual.Int(from, to, _animationDuration, value => _text.text = value.ToString());
    }
  }
}
using System;
using Game.Views;
using Modules.Money;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Presenters
{
  public class MoneyPresenter : IInitializable, IDisposable
  {
    public Vector3 CoinIconPosition => _moneyView.IconPosition;
    
    private readonly MoneyView _moneyView;
    private readonly IMoneyStorage _moneyStorage;

    private int _currentMoney;

    public MoneyPresenter(MoneyView moneyView, IMoneyStorage moneyStorage)
    {
      _moneyView = moneyView;
      _moneyStorage = moneyStorage;
    }

    public void Initialize()
    {
      _moneyStorage.OnMoneySpent += UpdateView;

      UpdateView(_moneyStorage.Money, 0);
    }

    public void Dispose()
    {
      _moneyStorage.OnMoneySpent -= UpdateView;
    }

    public void UpdateViewWithAnim()
    {
      _moneyView.UpdateMoneyWithAnim(_currentMoney, _moneyStorage.Money);
      _currentMoney = _moneyStorage.Money;
    }

    private void UpdateView(int newValue, int prevValue)
    {
      _moneyView.UpdateMoney(_moneyStorage.Money.ToString());
      _currentMoney = newValue;
    }
  }
}
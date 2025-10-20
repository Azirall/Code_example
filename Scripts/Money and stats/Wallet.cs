using System;
using UnityEngine;

public class Wallet
{
    public event Action moneyChanged;
    private int _money = 0;
    public int GetMoney => _money;

    public void AddMoney(int count) {

        _money += count;
        moneyChanged?.Invoke();

    }
    public void SubtractMoney(int count) {

        _money -= count;
        moneyChanged?.Invoke();
        Debug.Log(_money);

    }
}

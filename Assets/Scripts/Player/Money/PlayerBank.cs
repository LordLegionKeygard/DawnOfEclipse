using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBank : MonoBehaviour
{
    public static event Action<int> OnCoinsValueChangedActionEvent;
    public int Coins;

    private void Start()
    {
        OnCoinsValueChangedActionEvent?.Invoke(this.Coins);
    }

    public void AddCoins(int amount)
    {
        this.Coins += amount;

        OnCoinsValueChangedActionEvent?.Invoke(this.Coins);
    }

    public void SpendCoins(int amount)
    {
        this.Coins -= amount;

        OnCoinsValueChangedActionEvent?.Invoke(this.Coins);
    }

    public bool isEnoughCoins(int amount)
    {
        return amount <= Coins;
    }
}

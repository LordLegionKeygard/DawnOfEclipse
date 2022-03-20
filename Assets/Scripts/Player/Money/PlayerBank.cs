using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerBank : MonoBehaviour
{
    public int Coins;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _coinsShopText;

    private void OnEnable()
    {
        CustomEvents.OnChangeCoins += ChangeCoins;
        UpdateCoinsText();
    }

    public void ChangeCoins(int amount)
    {
        Coins += amount;
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        _coinsText.text = Coins.ToString();
        _coinsShopText.text = Coins.ToString();
    }

    private void OnDisable()
    {
        CustomEvents.OnChangeCoins -= ChangeCoins;
    }
}

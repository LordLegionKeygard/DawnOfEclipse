using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdatePlayerCoins : MonoBehaviour
{
    [SerializeField] private Text _coinsText;

    [SerializeField] private TextMeshProUGUI _coinsShopText;
    private void Start()
    {
        PlayerBank.OnCoinsValueChangedActionEvent += UpdateCoinsText;
    }

    private void UpdateCoinsText(int amount)
    {
        _coinsText.text = ("Coins: " + amount.ToString());
        _coinsShopText.text = amount.ToString();
    }
}

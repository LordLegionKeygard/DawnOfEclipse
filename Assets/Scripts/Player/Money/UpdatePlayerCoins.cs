using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdatePlayerCoins : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _coinsShopText;
    private void Start()
    {
        PlayerBank.OnCoinsValueChangedActionEvent += UpdateCoinsText;
    }

    private void UpdateCoinsText(int amount)
    {
        _coinsText.text = (amount.ToString());
        _coinsShopText.text = amount.ToString();
    }
}

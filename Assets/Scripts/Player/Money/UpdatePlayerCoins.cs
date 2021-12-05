using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerCoins : MonoBehaviour
{
    [SerializeField] private Text _coinsText;
    private void Start()
    {
        PlayerBank.OnCoinsValueChangedActionEvent += UpdateCoinsText;
    }

    private void UpdateCoinsText(int amount)
    {
        _coinsText.text = ("Coins: " + amount.ToString());
    }
}

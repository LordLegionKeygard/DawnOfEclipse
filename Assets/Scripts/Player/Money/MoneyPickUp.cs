using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickUp : MonoBehaviour
{
    private PlayerBank _playerBank;


    public int DropCoins;
    private void Start()
    {
        _playerBank = FindObjectOfType<PlayerBank>();
    }
    public void PickUp(int amount)
    {
        Debug.Log("PickUp " + amount + "coins");
        _playerBank.AddCoins(amount);
        Destroy(gameObject);
    }
}

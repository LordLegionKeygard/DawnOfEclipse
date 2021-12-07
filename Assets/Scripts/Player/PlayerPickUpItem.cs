using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpItem : MonoBehaviour
{
    [SerializeField] private PlayerBank _playerBank;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("DropItem") && (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.E)))
        {
            other.gameObject.GetComponent<ItemPickup>().PickUp();
        }
        if (other.gameObject.CompareTag("CoinPurse") && (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.E)))
        {
            var _coins = other.gameObject.GetComponent<CoinPurse>().CoinsInPurse;
            _playerBank.AddCoins(_coins);
            Destroy(other.gameObject);
        }
    }
}

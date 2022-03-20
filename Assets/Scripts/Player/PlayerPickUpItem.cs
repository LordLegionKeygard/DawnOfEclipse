using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpItem : MonoBehaviour
{
    [SerializeField] private bool _canPickUp = true;
    [SerializeField] private float _defaultTime;
    private float _timer;

    private void Start()
    {
        _timer = _defaultTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("DropItem") && (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.E)))
        {
            if (_canPickUp)
            {
                other.gameObject.GetComponent<ItemPickup>().PickUp();
                _canPickUp = false;
            }

        }
        if (other.gameObject.CompareTag("CoinPurse") && (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.E)))
        {
            var coins = other.gameObject.GetComponent<CoinPurse>().CoinsInPurse;
            CustomEvents.FireChangeCoins(coins);
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (!_canPickUp)
        {
            _timer -= Time.deltaTime;
            if (_timer < 0)
            {
                _canPickUp = true;
                _timer = _defaultTime;
            }
        }
    }
}

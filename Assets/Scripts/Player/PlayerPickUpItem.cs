using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpItem : MonoBehaviour
{
    [SerializeField] private PlayerAnimatorManager _playerAnimatorManager;
    [SerializeField] private bool _canPickUp = true;
    [SerializeField] private float _defaultTime;
    private float _timer;
    private bool _pickUpButton;

    private void OnEnable()
    {
        CustomEvents.OnPickUp += PickUpTogle;
    }

    private void Start()
    {
        _timer = _defaultTime;
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

    private void PickUpTogle(bool state)
    {
        _pickUpButton = state;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_pickUpButton || !_canPickUp) return;

        if (other.gameObject.TryGetComponent(out ItemPickup pickUp))
        {
            pickUp.PickUp();
            _playerAnimatorManager.AnimatorPickUpToggle();
        }
        if (other.gameObject.TryGetComponent(out CoinPurse coinPurse))
        {
            var coins = coinPurse.CoinsInPurse;
            CustomEvents.FireChangeCoins(coins);
            Destroy(other.gameObject);
        }
        _canPickUp = false;
    }

    private void OnDisable()
    {
        CustomEvents.OnPickUp -= PickUpTogle;
    }
}

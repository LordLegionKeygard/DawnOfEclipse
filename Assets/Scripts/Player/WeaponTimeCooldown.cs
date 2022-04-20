using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTimeCooldown : MonoBehaviour
{
    private PlayerInputController _playerInputController;
    private FistDamageColliderController _fistDamageColliderController;


    private void Awake()
    {
        _playerInputController = GetComponent<PlayerInputController>();
        _fistDamageColliderController = GetComponent<FistDamageColliderController>();
    }

    public void NoWeapon()
    {
        _fistDamageColliderController.FistDamageColliderToggle(true);
        _playerInputController.TimeR1 = 0.4f;
        _playerInputController.TimeR2 = 0.5f;
        _playerInputController.TimeL1 = 1.6f;
        _playerInputController.StaminaForR1 = 50f;
        _playerInputController.StaminaForR2 = 50f;
    }
    public void GreatSword()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        _playerInputController.TimeR1 = 0.3f;
        _playerInputController.TimeR2 = 0.5f;
        _playerInputController.TimeL1 = 0.7f;
        _playerInputController.StaminaForR1 = 150f;
        _playerInputController.StaminaForR2 = 300f;
    }
    public void Sword()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        _playerInputController.TimeR1 = 0.3f;
        _playerInputController.TimeR2 = 1.5f;
        _playerInputController.TimeL1 = 0.7f;
        _playerInputController.StaminaForR1 = 100f;
        _playerInputController.StaminaForR2 = 100f;
    }
}

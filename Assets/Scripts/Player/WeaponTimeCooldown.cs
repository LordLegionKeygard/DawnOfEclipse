using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTimeCooldown : MonoBehaviour
{
    [SerializeField] private PlayerInputController _playerInputController;
    [SerializeField] private FistDamageColliderController _fistDamageColliderController;

    public void NoWeapon()
    {
        _fistDamageColliderController.FistDamageColliderToggle(true);
        _playerInputController.TimeR1 = 0.4f;
        _playerInputController.TimeR2 = 0.5f;
        _playerInputController.StaminaForR1 = 5f;
        _playerInputController.StaminaForR2 = 5f;
    }
    public void GreatSword()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        _playerInputController.TimeR1 = 0.7f;
        _playerInputController.TimeR2 = 0.8f;
        _playerInputController.StaminaForR1 = 15f;
        _playerInputController.StaminaForR2 = 30f;
    }
    public void StraightSword()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        _playerInputController.TimeR1 = 0.35f;
        _playerInputController.TimeR2 = 0.5f;
        _playerInputController.StaminaForR1 = 10f;
        _playerInputController.StaminaForR2 = 10f;
    }
    public void Hammer()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        _playerInputController.TimeR1 = 0.3f;
        _playerInputController.TimeR2 = 0.5f;
        _playerInputController.StaminaForR1 = 10f;
        _playerInputController.StaminaForR2 = 10f;
    }
    public void Daggers()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        _playerInputController.TimeR1 = 0.45f;
        _playerInputController.TimeR2 = 0.5f;
        _playerInputController.StaminaForR1 = 5f;
        _playerInputController.StaminaForR2 = 5f;
    }
}

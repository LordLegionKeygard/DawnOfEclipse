using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTimeCooldown : MonoBehaviour
{
    [SerializeField] private PlayerInputController _playerInputController;

    public void NoWeapon()
    {
        _playerInputController.TimeR1 = 0.4f;
        _playerInputController.TimeR2 = 0.5f;
        _playerInputController.TimeL1 = 1.6f;
        _playerInputController.StaminaForR1 = 50f;
        _playerInputController.StaminaForR2 = 50f;
    }
    public void GreatSword()
    {
        _playerInputController.TimeR1 = 0.3f;
        _playerInputController.TimeR2 = 0.5f;
        _playerInputController.TimeL1 = 0.7f;
        _playerInputController.StaminaForR1 = 150f;
        _playerInputController.StaminaForR2 = 300f;
    }
    public void LongSword()
    {
        _playerInputController.TimeR1 = 0.3f;
        _playerInputController.TimeR2 = 1.5f;
        _playerInputController.TimeL1 = 0.7f;
        _playerInputController.StaminaForR1 = 100f;
        _playerInputController.StaminaForR2 = 100f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsInfo : MonoBehaviour
{
    [SerializeField] private PlayerInputController _playerInputController;
    [SerializeField] private FistDamageColliderController _fistDamageColliderController;

    [Header("Stamina")]
    public int StaminaAttack = 5;
    public int StaminaRoll = 5;
    public int StaminaJump = 5;

    [Header("Time")]
    public float TimeR1 = 0.4f;
    public float TimeR2 = 0.5f;

    public void NoWeapon()
    {
        _fistDamageColliderController.FistDamageColliderToggle(true);
        TimeR1 = 0.4f;
        TimeR2 = 0.5f;
        StaminaAttack = 3;
        StaminaJump = 15;
        StaminaRoll = 7;
    }

    public void Axe()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0;
        TimeR2 = 0;
        StaminaAttack = 12;
        StaminaJump = 32;
        StaminaRoll = 12;
    }

    public void Daggers()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0.45f;
        TimeR2 = 0.5f;
        StaminaAttack = 7;
        StaminaJump = 20;
        StaminaRoll = 7;
    }

    public void Claws()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0.4f;
        TimeR2 = 0.5f;
        StaminaAttack = 7;
        StaminaJump = 20;
        StaminaRoll = 7;
    }

    public void GreatHammer()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0;
        TimeR2 = 0;
        StaminaAttack = 17;
        StaminaJump = 35;
        StaminaRoll = 17;
    }
    public void GreatAxe()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0;
        TimeR2 = 0;
        StaminaAttack = 17;
        StaminaJump = 35;
        StaminaRoll = 17;
    }

    public void GreatSword()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0.7f;
        TimeR2 = 0.8f;
        StaminaAttack = 15;
        StaminaJump = 35;
        StaminaRoll = 15;
    }

    public void Hammer()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0.3f;
        TimeR2 = 0.5f;
        StaminaAttack = 12;
        StaminaJump = 32;
        StaminaRoll = 12;
    }
    public void Spear()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0.7f;
        TimeR2 = 0.8f;
        StaminaAttack = 12;
        StaminaJump = 32;
        StaminaRoll = 12;
    }

    public void StraightSword()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0.35f;
        TimeR2 = 0.5f;
        StaminaAttack = 10;
        StaminaJump = 26;
        StaminaRoll = 10;
    }

    public void Staff()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0.7f;
        TimeR2 = 0.9f;
        StaminaAttack = 5;
        StaminaJump = 15;
        StaminaRoll = 7;
    }

    public void Bow()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 2f;
        TimeR2 = 0.5f;
        StaminaAttack = 12;
        StaminaJump = 15;
        StaminaRoll = 7;
    }
}

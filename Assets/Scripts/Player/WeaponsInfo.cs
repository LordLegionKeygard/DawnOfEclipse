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
        StaminaJump = 30;
        StaminaRoll = 14;
    }

    public void Axe()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0;
        TimeR2 = 0;
        StaminaAttack = 25;
        StaminaJump = 65;
        StaminaRoll = 25;
    }

    public void CurvedSword()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0;
        TimeR2 = 0;
        StaminaAttack = 20;
        StaminaJump = 52;
        StaminaRoll = 20;
    }

    public void CurvedGreatSword()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0;
        TimeR2 = 0;
        StaminaAttack = 30;
        StaminaJump = 70;
        StaminaRoll = 30;
    }

    public void Daggers()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0.45f;
        TimeR2 = 0.5f;
        StaminaAttack = 14;
        StaminaJump = 40;
        StaminaRoll = 14;
    }

    public void Fists()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0;
        TimeR2 = 0;
        StaminaAttack = 18;
        StaminaJump = 47;
        StaminaRoll = 18;
    }

    public void GreatHammer()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0;
        TimeR2 = 0;
        StaminaAttack = 35;
        StaminaJump = 70;
        StaminaRoll = 35;
    }
    public void GreatAxe()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0;
        TimeR2 = 0;
        StaminaAttack = 35;
        StaminaJump = 70;
        StaminaRoll = 35;
    }

    public void GreatSword()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0.7f;
        TimeR2 = 0.8f;
        StaminaAttack = 30;
        StaminaJump = 70;
        StaminaRoll = 30;
    }

    public void Halberd()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0;
        TimeR2 = 0;
        StaminaAttack = 30;
        StaminaJump = 70;
        StaminaRoll = 30;
    }

    public void Hammer()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0.3f;
        TimeR2 = 0.5f;
        StaminaAttack = 25;
        StaminaJump = 65;
        StaminaRoll = 25;
    }
    public void Katana()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0;
        TimeR2 = 0;
        StaminaAttack = 24;
        StaminaJump = 62;
        StaminaRoll = 24;
    }
    public void Spear()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0;
        TimeR2 = 0;
        StaminaAttack = 25;
        StaminaJump = 65;
        StaminaRoll = 25;
    }

    public void StraightSword()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0.35f;
        TimeR2 = 0.5f;
        StaminaAttack = 20;
        StaminaJump = 52;
        StaminaRoll = 20;
    }

    public void PiercingSword()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0;
        TimeR2 = 0;
        StaminaAttack = 20;
        StaminaJump = 52;
        StaminaRoll = 20;
    }
    public void UltraGreatSword()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0;
        TimeR2 = 0;
        StaminaAttack = 45;
        StaminaJump = 90;
        StaminaRoll = 45;
    }

    public void Staff()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 0.7f;
        TimeR2 = 0.9f;
        StaminaAttack = 10;
        StaminaJump = 30;
        StaminaRoll = 14;
    }

    public void Bow()
    {
        _fistDamageColliderController.FistDamageColliderToggle(false);
        TimeR1 = 2f;
        TimeR2 = 0.5f;
        StaminaAttack = 25;
        StaminaJump = 30;
        StaminaRoll = 14;
    }
}

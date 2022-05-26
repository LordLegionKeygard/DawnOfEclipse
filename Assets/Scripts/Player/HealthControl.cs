using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthControl : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _healthBarBack;
    private Animator _animator;
    private PlayerInputController _playerInputController;
    private PlayerAnimatorManager _playerAnimatorManager;
    private ArmorControl _armorControl;
    private MagicArmorControl _magicArmorControl;
    private StaminaControl _staminaControl;
    private CharacterController _characterController;
    public static bool IsDeath;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        _staminaControl = GetComponent<StaminaControl>();
        _animator = GetComponent<Animator>();
        _playerInputController = GetComponent<PlayerInputController>();
        _armorControl = GetComponent<ArmorControl>();
        _magicArmorControl = GetComponent<MagicArmorControl>();
    }

    public void CalculateHealth()
    {
        CurrentHealth = MaxHealth;
        _healthBar.maxValue = MaxHealth;
        _healthBar.value = MaxHealth;

        _healthBarBack.maxValue = MaxHealth;
        _healthBarBack.value = MaxHealth;
    }

    public void CalculateDamage(float damage, DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.PhysDamage:
                var enemyDamage = damage * 70 / _armorControl.CurrentArmor;
                TakeDamage(enemyDamage);
                break;
            case DamageType.MageDamage:
                enemyDamage = damage * 70 / _magicArmorControl.CurrentMagicArmor;
                TakeDamage(enemyDamage);
                break;
        }
    }

    private void TakeDamage(float damage)
    {
        Debug.Log("Enemy" + " dealt " + damage + " damage");
        CurrentHealth = CurrentHealth - (int)damage;
        if (_playerInputController.IsBlock)
        {
            _staminaControl.UseStamina((int)damage);
            _playerAnimatorManager.BlockReact();
        }
        UpdateSlider();
    }

    public void UpdateSlider()
    {
        _healthBar.value = CurrentHealth;
        _healthBarBack.DOValue(CurrentHealth, 1f, false);
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (CurrentHealth <= 0)
        {
            IsDeath = true;
            RandomDeath();
            CustomEvents.FirePlayerDeath();
        }
    }
    private void RandomTakeDamage()
    {
        int randomState = UnityEngine.Random.Range(0, 4);
        if (randomState == 0)
        {
            _animator.SetTrigger("takeDamage");
        }
        else if (randomState == 1)
        {
            _animator.SetTrigger("takeDamage1");
        }
    }

    private void RandomDeath()
    {
        int randomState = UnityEngine.Random.Range(0, 2);
        if (randomState == 0)
        {
            _animator.SetTrigger("death");
        }
        else if (randomState == 1)
        {
            _animator.SetTrigger("death1");
        }
        _characterController.enabled = false;
    }
}

[System.Serializable]
public enum DamageType
{
    PhysDamage = 0,
    MageDamage = 1
}

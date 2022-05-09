using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image healthBarImage;
    private Animator _animator;
    private PlayerInputController _playerInputController;
    private PlayerAnimatorManager _playerAnimatorManager;
    private ArmorControl _armorControl;
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
    }
    private void Start()
    {
        CurrentHealth = MaxHealth;
        healthBar.maxValue = MaxHealth;
        healthBar.value = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        var enemyDamage = (1 - (_armorControl.CurrentArmor / damage)) * damage;
        Debug.Log("Enemy" + " dealt " + enemyDamage + " damage");
        if (enemyDamage >= 0)
        {
            CurrentHealth = CurrentHealth - (int)enemyDamage;
        }
        else if (_playerInputController.IsBlock == false && enemyDamage < 0)
        {
            CurrentHealth = CurrentHealth - (int)(damage * 0.05f);
            RandomTakeDamage();
        }
        else if (_playerInputController.IsBlock == true)
        {
            _staminaControl.UseStamina((int)damage);
            _playerAnimatorManager.BlockReact();
        }
        CheckDeath();
        // UpdateHealthColorBar();
    }

    private void Update()
    {
        if (healthBar.value == CurrentHealth) return;

        if (healthBar.value > CurrentHealth)
        {
            healthBar.value -= Time.deltaTime * 50;
        }
        if (healthBar.value < CurrentHealth)
        {
            healthBar.value += Time.deltaTime * 50;
        }
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

    // public void UpdateHealthColorBar()
    // {
    //     Color healthGreenColor = new Color(0.01176471f, 0.8117647f, 0.1607843f);
    //     float healthBarPercent = (float)CurrentHealth / (float)MaxHealth;
    //     healthBarImage.color = Color.Lerp(Color.red, healthGreenColor, healthBarPercent);
    // }

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

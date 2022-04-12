using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : CharacterStats
{
    public static event Action PlayerDeathEvent;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image healthBarImage;
    private Animator animator;
    private PlayerInputController _playerInputController;
    private PlayerAnimatorManager playerAnimatorManager;
    private ArmorControl armorControl;
    private StaminaControl staminaControl;
    private CharacterController characterController;
    public static bool IsDeath;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        staminaControl = GetComponent<StaminaControl>();
        animator = GetComponent<Animator>();
        _playerInputController = GetComponent<PlayerInputController>();
        armorControl = GetComponent<ArmorControl>();
    }
    private void Start()
    {
        CurrentHealth = MaxHealth;
        CurrentHealth = MaxHealth;
        healthBar.maxValue = MaxHealth;
        healthBar.value = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        var enemyDamage = (1 - (armorControl.CurrentArmor / damage)) * damage;
        Debug.Log(enemyDamage);
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
            staminaControl.UseStamina((int)damage * 10);
            playerAnimatorManager.BlockReact();
        }
        CheckDeath();
        UpdateHealthColorBar();
    }

    private void Update()
    {
        if (healthBar.value == CurrentHealth)
        {
            return;
        }
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
            RandomDeath();
        }
    }

    public void UpdateHealthColorBar()
    {
        Color healthGreenColor = new Color(0.01176471f, 0.8117647f, 0.1607843f);
        float healthBarPercent = (float)CurrentHealth / (float)MaxHealth;
        healthBarImage.color = Color.Lerp(Color.red, healthGreenColor, healthBarPercent);
    }

    private void RandomTakeDamage()
    {
        int randomState = UnityEngine.Random.Range(0, 4);
        if (randomState == 0)
        {
            animator.SetTrigger("takeDamage");
        }
        else if (randomState == 1)
        {
            animator.SetTrigger("takeDamage1");
        }
    }

    private void RandomDeath()
    {
        int randomState = UnityEngine.Random.Range(0, 2);
        if (randomState == 0)
        {
            animator.SetTrigger("death");
        }
        else if (randomState == 1)
        {
            animator.SetTrigger("death1");
        }
        characterController.enabled = false;
        PlayerDeathEvent?.Invoke();
    }
}

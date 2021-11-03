using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : CharacterStats
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private GameObject combatCollider;
    private Animator animator;
    private EnemyManager[] enemyManagers;
    private PlayerController playerController;
    private PlayerAnimatorManager playerAnimatorManager;
    private ArmorControl armorControl;
    private StaminaControl staminaControl;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        staminaControl = GetComponent<StaminaControl>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        armorControl = GetComponent<ArmorControl>();
    }
    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(float damage)
    {

        var enemyDamage = (1 - (armorControl.currentArmor / damage)) * damage;
        Debug.Log(enemyDamage);
        if (enemyDamage >= 0)
        {
            currentHealth = currentHealth - (int)enemyDamage;
        }
        else if (playerController.block == false && enemyDamage < 0)
        {
            currentHealth = currentHealth - (int)(damage * 0.05f);
            RandomTakeDamage();
        }
        else if (playerController.block == true)
        {
            staminaControl.UseStamina((int)damage * 10);
            playerAnimatorManager.BlockReact();
        }
        else if (currentHealth <= 0)
        {
            RandomDeath();
        }
        UpdateHealthColorBar();
    }

    private void Update()
    {
        if (healthBar.value == currentHealth)
        {
            return;
        }
        if (healthBar.value > currentHealth)
        {
            healthBar.value -= Time.deltaTime * 50;
        }
        if (healthBar.value < currentHealth)
        {
            healthBar.value += Time.deltaTime * 50;
        }
    }

    public void UpdateHealthColorBar()
    {
        Color healthGreenColor = new Color(0.01176471f, 0.8117647f, 0.1607843f);
        float healthBarPercent = (float)currentHealth / (float)maxHealth;
        healthBarImage.color = Color.Lerp(Color.red, healthGreenColor, healthBarPercent);
    }

    private void RandomTakeDamage()
    {
        int randomState = Random.Range(0, 4);
        if (randomState == 0)
        {
            animator.SetTrigger("takeDamage");
        }
        else if (randomState == 1)
        {
            animator.SetTrigger("takeDamage1");
        }
        else if (randomState == 2)
        {

        }
        else if (randomState == 3)
        {

        }
    }

    private void RandomDeath()
    {
        int randomState = Random.Range(0, 2);
        if (randomState == 0)
        {
            animator.SetTrigger("death");
        }
        if (randomState == 1)
        {
            animator.SetTrigger("death1");
        }
        characterController.enabled = false;
        combatCollider.SetActive(false);
        enemyManagers = FindObjectsOfType<EnemyManager>();
        foreach (var spawn in enemyManagers)
        {
            spawn.ReturnToSpawn();
        }
    }
}

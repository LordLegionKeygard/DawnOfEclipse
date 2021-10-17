using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : CharacterStats
{
    [SerializeField] private Slider healthBar;
    private Animator animator;

    [SerializeField] private Image healthBarImage;
    private PlayerController playerController;

    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (playerController.block == false)
        {
            currentHealth = currentHealth - damage;
            healthBar.value = currentHealth;
            UpdateHealthColorBar();

            RandomTakeDamage();
        }

        if (currentHealth <= 0)
        {
            RandomDeath();
        }
    }

    private void UpdateHealthColorBar()
    {
        Color healthGreenColor = new Color(0.01176471f, 0.8117647f, 0.1607843f);
        float healthBarPercent = (float)currentHealth / (float)maxHealth;

        healthBarImage.color = Color.Lerp(Color.red, healthGreenColor, healthBarPercent);

    }

    private void RandomTakeDamage()
    {
        int randomState = Random.Range(0, 6);
        if (randomState == 0)
        {
            animator.SetTrigger("takeDamage");
        }
        if (randomState == 1)
        {
            animator.SetTrigger("takeDamage1");
        }
        if (randomState == 2)
        {
            
        }
        if (randomState == 3)
        {
            
        }
        if (randomState == 4)
        {
            
        }
        if (randomState == 5)
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
    }
}

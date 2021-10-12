using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : CharacterStats
{
    [SerializeField] private Slider healthBar;
    private Animator animator;
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

            RandomTakeDamage();
        }

        if (currentHealth <= 0)
        {
            RandomDeath();
        }
    }

    private void RandomTakeDamage()
    {
        int randomState = Random.Range(0, 2);
        if (randomState == 0)
        {
            animator.SetTrigger("takeDamage");
        }
        if (randomState == 1)
        {
            animator.SetTrigger("takeDamage1");
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

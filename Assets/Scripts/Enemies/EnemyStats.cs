using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    // [SerializeField] private UIEnemyHealthBar enemyHealthBar;
    private Collider col;
    private NewEnemyAnimatorManager _newEnemyAnimatorManager;  
    private CameraLockOnTarget cameraLockOnTarget;
    private Rigidbody rb;
    private MobSpawner mobSpawner;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cameraLockOnTarget = FindObjectOfType<CameraLockOnTarget>();
        col = GetComponent<Collider>();       
    }

    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        // enemyHealthBar.SetMaxHealth(maxHealth);
        mobSpawner = GetComponentInParent<MobSpawner>();
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // enemyHealthBar.SetHealth(currentHealth);

        RandomTakeDamage();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        _newEnemyAnimatorManager.PlayerTargetAnimation("death");
        col.enabled = false;       
        rb.isKinematic = true;
        // enemyManager.enabled = false;
        cameraLockOnTarget.TargetDeath();
        Respawn();
        Destroy(gameObject, 8f);
    }

    private void Respawn()
    {
        mobSpawner.spawn = true;
    }

    private void RandomTakeDamage()
    {
        int randomState = Random.Range(0, 3);
        if (randomState == 0)
        {
            // _newEnemyAnimatorManager.PlayerTargetAnimation("SwordTakeDamage", true);
        }
        if (randomState == 1){}
        else if (randomState == 2){}
    }
}

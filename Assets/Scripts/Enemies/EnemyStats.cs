using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField] private Collider[] colliders;
    private EnemyAnimatorManager enemyAnimatorManager;

    private CameraLockOnTarget cameraLockOnTarget;

    private EnemyManager enemyManager;

    private Rigidbody rb;

    private void Awake()
    {
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        rb = GetComponent<Rigidbody>();
        enemyManager = GetComponent<EnemyManager>();
        cameraLockOnTarget = FindObjectOfType<CameraLockOnTarget>();
    }

    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        enemyAnimatorManager.PlayerTargetAnimation("SwordTakeDamage", true);

        if(currentHealth <= 0)
        {
            enemyAnimatorManager.PlayerTargetAnimation("Falling Back Death", true);
            foreach (var col in colliders)
            {
                col.enabled = false;
            }
            rb.isKinematic = true;
            enemyManager.enabled = false;
            cameraLockOnTarget.TargetDeath();
        }
    }
}

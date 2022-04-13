using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField] private UIEnemyHealthBar _enemyHealthBar;
    private  NewEnemyAnimatorManager _newEnemyAnimatorManager;
    private  CharacterController _characterController;
    private  MobSpawner _mobSpawner;
    private EnemyVFXController _enemyVFXController;

    private void Awake()
    {
        _newEnemyAnimatorManager = GetComponent<NewEnemyAnimatorManager>();
        _characterController = GetComponent<CharacterController>();
        _enemyVFXController = GetComponent<EnemyVFXController>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
        _enemyHealthBar.SetMaxHealth(MaxHealth);
        _mobSpawner = GetComponentInParent<MobSpawner>();
    }

    public void TakeDamage(int damage)
    {
        _enemyVFXController.TakeDamageVFX();
        CurrentHealth -= damage;
        _enemyHealthBar.SetHealth(CurrentHealth);
        RandomTakeDamage();
        if (CurrentHealth <= 0) { Death(); }
    }

    private void Death()
    {
        _newEnemyAnimatorManager.PlayerTargetAnimation("death");
        _characterController.enabled = false;
        CustomEvents.FireCameraLockOnTargetDeath();
        Respawn();
        Destroy(gameObject, 8f);
    }

    private void Respawn()
    {
        _mobSpawner.CanSpawn = true;
    }

    private void RandomTakeDamage()
    {
        int randomState = Random.Range(0, 3);
        if (randomState == 0)
        {
            // _newEnemyAnimatorManager.PlayerTargetAnimation("SwordTakeDamage");
        }
    }
}

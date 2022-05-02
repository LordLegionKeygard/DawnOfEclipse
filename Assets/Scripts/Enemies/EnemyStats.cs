using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyStats : CharacterStats
{
    private AIPath _aiPath;
    [SerializeField] private UIEnemyHealthBar _enemyHealthBar;
    private NewEnemyAnimatorManager _newEnemyAnimatorManager;
    private CharacterController _characterController;
    private MobSpawner _mobSpawner;
    private EnemyVFXController _enemyVFXController;

    private bool _death;

    private void Awake()
    {
        _newEnemyAnimatorManager = GetComponent<NewEnemyAnimatorManager>();
        _characterController = GetComponent<CharacterController>();
        _enemyVFXController = GetComponent<EnemyVFXController>();
        _aiPath = GetComponent<AIPath>();
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
        UpdateSlider();
        RandomTakeDamage();
    }

    public void UpdateSlider()
    {
        if(_death) return;
        _enemyHealthBar.SetHealth(CurrentHealth);
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (CurrentHealth <= 0 && !_death)
        {
            _death = true;
            Death();
        }
    }

    private void Death()
    {
        _newEnemyAnimatorManager.PlayerTargetAnimation("death");
        _characterController.enabled = false;
        CustomEvents.FireCameraLockOnTargetDeath();
        // RotateBodyAfterDeath();
        _aiPath.enabled = false;
        Respawn();
        Destroy(gameObject, 8f);
    }

    // private void RotateBodyAfterDeath()
    // {
    //     transform.Rotate(new Vector3(-8.11f, transform.rotation.y, 1.61f), Space.World);
    // }

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

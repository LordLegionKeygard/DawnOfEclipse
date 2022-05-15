using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyStats : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    private AIPath _aiPath;
    [SerializeField] private UIEnemyHealthBar _enemyHealthBar;
    private NewEnemyAnimatorManager _newEnemyAnimatorManager;
    private CharacterController _characterController;
    private MobSpawner _mobSpawner;
    private EnemyVFXController _enemyVFXController;
    private EnemyLoot _enemyLoot;
    private EnemyLevel _enemyLevel;
    public bool Aggression;
    private bool _death;

    [Header("Defence")]
    [SerializeField] private int _enemyPhysDefence;
    [SerializeField] private int _enemyMagDefence;

    private void Awake()
    {
        _newEnemyAnimatorManager = GetComponent<NewEnemyAnimatorManager>();
        _characterController = GetComponent<CharacterController>();
        _enemyVFXController = GetComponent<EnemyVFXController>();
        _enemyLevel = GetComponent<EnemyLevel>();
        _aiPath = GetComponent<AIPath>();
        _enemyLoot = GetComponent<EnemyLoot>();
    }

    private void Start()
    {
        MaxHealth = _enemyLevel.EnemyInformation.Health[_enemyLevel.Level];
        _enemyPhysDefence = _enemyLevel.EnemyInformation.physDef[_enemyLevel.Level];
        _enemyMagDefence = _enemyLevel.EnemyInformation.magDef[_enemyLevel.Level];
        CurrentHealth = MaxHealth;
        _enemyHealthBar.SetMaxHealth(MaxHealth);
        _mobSpawner = GetComponentInParent<MobSpawner>();
    }

    public void CalculateDamage(float damage, DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.PhysDamage:
                var playerDamage = damage * 70 / _enemyPhysDefence;
                TakeDamage(playerDamage);
                break;
            case DamageType.MageDamage:
                playerDamage = damage * 70 / _enemyMagDefence;
                TakeDamage(playerDamage);
                break;
        }
    }

    private void TakeDamage(float damage)
    {
        if (damage <= 0) return;

        Aggression = true;
        _enemyVFXController.TakeDamageVFX();
        CurrentHealth -= (int)damage;
        UpdateSlider();
    }

    public void UpdateSlider()
    {
        if (_death) return;
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
        else RandomTakeDamage();
    }

    private void Death()
    {
        _enemyHealthBar.gameObject.SetActive(false);
        _newEnemyAnimatorManager.PlayerTargetAnimation("death");
        _characterController.enabled = false;
        CustomEvents.FireCameraLockOnTargetDeath();
        _enemyLoot.CalculateLoot();
        CustomEvents.FireChangeExperience(_enemyLevel.EnemyInformation.Exp[_enemyLevel.Level]);
        _aiPath.enabled = false;
        _mobSpawner.CanSpawn = true;
        Destroy(gameObject, 8f);
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

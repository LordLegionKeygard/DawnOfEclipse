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

    [SerializeField] private GameObject _floatingText;

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

    public void CalculateDamage(float damage, DamageType damageType, bool crit)
    {
        _enemyVFXController.TakeDamageVFX();
        
        if(_death) return;
    
        var popup = Mathf.Round(damage);
        ShowFloatingtext(popup.ToString(), damageType, crit);

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
        CurrentHealth -= (int)damage;
        UpdateSlider();
    }

    private void ShowFloatingtext(string text, DamageType damageType, bool crit)
    {
        var t = Instantiate(_floatingText, transform.position, Quaternion.identity, transform);
        t.GetComponent<TextMesh>().text = text;

        if (crit)
        {
            switch (damageType)
            {
                case DamageType.PhysDamage:
                    t.GetComponent<TextMesh>().color = new Color(1, 0.6745098f, 0, 1);
                    break;
                case DamageType.MageDamage:
                    t.GetComponent<TextMesh>().color = new Color(0, 0.4541306f, 1, 1);
                    break;
            }
        }
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

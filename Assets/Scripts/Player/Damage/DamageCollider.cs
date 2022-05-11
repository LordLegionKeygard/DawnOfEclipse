using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [Header("Current")]
    [SerializeField] private int _weaponDamage;
    [SerializeField] private float _physCritChance;

    [Header("Base")]
    [SerializeField] private float _basePhysCritChance;
    public int BaseWeaponDamage;

    [Header("Other")]
    private Collider _damageCollider;
    private bool _canDamage = true;

    private void Awake()
    {
        _damageCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        CustomEvents.OnEnabledDamageCollider += Damage;
        CustomEvents.OnUpdateBaseWeaponDamage += CalculateWeaponDamage;
        CalculateWeaponDamage();
    }

    private void CalculateWeaponDamage()
    {
        if (!_canDamage) return;

        _weaponDamage = (int)(BaseWeaponDamage * ((1 + 0.05f * ExperienceControl.CurrentLevel)) * (1 + (0.05f * CharacterStats.Strength)));

        CustomEvents.FireUpdateWeaponPhysDamage(_weaponDamage);
        CalculatePhysCritChance();
    }

    private void CalculatePhysCritChance()
    {
        _physCritChance = _basePhysCritChance * (1 + (0.05f * CharacterStats.Dexterity));
        CustomEvents.FireUpdateWeaponPhysCritChance(_physCritChance);
    }

    public void CanDamage(bool state)
    {
        _canDamage = state;
        if (state)
        {
            CalculateWeaponDamage();
        }
    }

    public void Damage(bool state)
    {
        _damageCollider.enabled = state;
    }

    private void OnTriggerStay(Collider collision)
    {
        var rnd = Random.Range(1, 100);
        if (collision.TryGetComponent(out EnemyStats enemyStats) && _canDamage)
        {
            if(rnd < _physCritChance)
            {
                enemyStats.CalculateDamage(_weaponDamage * 2, DamageType.PhysDamage);
                Debug.Log("Crit");
            }
            else
            {
                enemyStats.CalculateDamage(_weaponDamage, DamageType.PhysDamage);
            }
            
            Damage(false);
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnEnabledDamageCollider -= Damage;
        CustomEvents.OnUpdateBaseWeaponDamage -= CalculateWeaponDamage;
    }
}

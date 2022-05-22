using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysDamage : MonoBehaviour
{
    [Header("Current")]
    public int WeaponDamage;
    public float PhysCritChance;

    [Header("Base")]
    [SerializeField] private float BasePhysCritChance;
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

        WeaponDamage = (int)(BaseWeaponDamage * ((1 + 0.05f * ExperienceControl.CurrentLevel)) * (1 + (0.05f * CharacterStats.Strength)));

        CustomEvents.FireUpdateWeaponPhysDamage(WeaponDamage);
        CalculatePhysCritChance();
    }

    private void CalculatePhysCritChance()
    {
        PhysCritChance = BasePhysCritChance * (1 + (0.05f * CharacterStats.Dexterity));
        CustomEvents.FireUpdateWeaponPhysCritChance(PhysCritChance);
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
        if(_damageCollider == null) return;
        var rnd = Random.Range(1, 100);
        if (collision.TryGetComponent(out EnemyStats enemyTakeDamage) && _canDamage)
        {
            if(rnd < PhysCritChance)
            {
                enemyTakeDamage.CalculateDamage(WeaponDamage * 2, DamageType.PhysDamage);
                Debug.Log("Crit");
            }
            else
            {
                enemyTakeDamage.CalculateDamage(WeaponDamage, DamageType.PhysDamage);
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

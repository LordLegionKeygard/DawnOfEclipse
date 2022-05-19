using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentDamage : MonoBehaviour
{
    public static CurrentDamage CurrentDamageS;
    public int CurrentWeaponPhysDamage;
    public float CurrentPhysCritChance;
    public int CurrentWeaponMagicDamage;
    public float CurrentMageCritChance;

    private void Awake()
    {
        CurrentDamageS = this;
    }

    private void OnEnable()
    {
        CustomEvents.OnUpdateWeaponPhysDamage += WeaponPhysDamage;
        CustomEvents.OnUpdateWeaponPhysCritChance += PhysCritChance;
        CustomEvents.OnUpdateWeaponMageDamage += WeaponMagicDamage;
        CustomEvents.OnUpdateWeaponMageCritChance += MageCritChance;
    }

    private void WeaponPhysDamage(int number)
    {
        CurrentWeaponPhysDamage = number;
    }
    private void PhysCritChance(float number)
    {
        CurrentPhysCritChance = number;
    }
    private void WeaponMagicDamage(int number)
    {
        CurrentWeaponMagicDamage = number;
    }
    private void MageCritChance(float number)
    {
        CurrentMageCritChance = number;
    }

    private void OnDestroy()
    {
        CustomEvents.OnUpdateWeaponPhysDamage -= WeaponPhysDamage;
        CustomEvents.OnUpdateWeaponPhysCritChance -= PhysCritChance;
        CustomEvents.OnUpdateWeaponMageDamage -= WeaponMagicDamage;
        CustomEvents.OnUpdateWeaponMageCritChance -= MageCritChance;
    }
}

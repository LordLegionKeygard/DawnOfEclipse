using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDamage : MonoBehaviour
{
    [Header("Current")]
    [SerializeField] private int _weaponMagicDamage;
    [SerializeField] private float _magCritChance;

    [Header("Base")]
    [SerializeField] private float _baseMagCritChance;
    public int BaseWeaponMagicDamage;

    [Header("Other")]
    private bool _canDamage = true;

    private void OnEnable()
    {
        CustomEvents.OnUpdateBaseWeaponDamage += CalculateWeaponMagicDamage;
        CalculateWeaponMagicDamage();
    }

    private void CalculateWeaponMagicDamage()
    {
        if (!_canDamage) return;

        _weaponMagicDamage = (int)((BaseWeaponMagicDamage * (Mathf.Pow((float)(1 + 0.05f * ExperienceControl.CurrentLevel), 2) * (Mathf.Pow((float)(1 + 0.05f * CharacterStats.Intelligence), 2)))));

        CustomEvents.FireUpdateWeaponMageDamage(_weaponMagicDamage);
        CalculateMagCritChance();
    }
    private void CalculateMagCritChance()
    {
        _magCritChance = _baseMagCritChance * (1 + (0.05f * CharacterStats.Wisdom));
        CustomEvents.FireUpdateWeaponMageCritChance(_magCritChance);
    }

    public void CanDamage(bool state)
    {
        _canDamage = state;
        if (state)
        {
            CalculateWeaponMagicDamage();
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnUpdateBaseWeaponDamage -= CalculateWeaponMagicDamage;
    }
}

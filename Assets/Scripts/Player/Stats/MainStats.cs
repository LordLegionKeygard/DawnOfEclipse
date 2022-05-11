using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _mainStats;
    [SerializeField] private HealthControl _healthControl;
    [SerializeField] private ManaControl _manaControl;
    [SerializeField] private StaminaControl _staminaControl;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PotionsControl _potionControl;
    [SerializeField] private int _currentWeaponPhysDamage;
    [SerializeField] private int _currentWeaponMageDamage;
    [SerializeField] private float _currentWeaponPhysCritChance;
    [SerializeField] private float _currentWeaponMageCritChance;

    private void OnEnable()
    {
        CustomEvents.OnUpdateAllStats += UpdateAllMainStats;
        CustomEvents.OnUpdateWeaponPhysDamage += UpdateWeaponPhysDamage;
        CustomEvents.OnUpdateWeaponMageDamage += UpdateWeaponMageDamage;
        CustomEvents.OnUpdateWeaponPhysCritChance += UpdateWeaponPhysCritChance;
        CustomEvents.OnUpdateWeaponMageCritChance += UpdateWeaponMageCritChance;
    }

    private void UpdateAllMainStats()
    {
        _mainStats[0].text = _healthControl.MaxHealth.ToString();
        _mainStats[1].text = _manaControl.MaxMana.ToString();
        _mainStats[2].text = _staminaControl.MaxStamina.ToString();
        _mainStats[3].text = (_playerMovement.CurrentSpeed + _potionControl.PotionSpeed).ToString();
        _mainStats[4].text = _currentWeaponPhysDamage.ToString();
        _mainStats[5].text = _currentWeaponMageDamage.ToString();
        _mainStats[8].text = _currentWeaponPhysCritChance.ToString() + "%";
        _mainStats[9].text = _currentWeaponMageCritChance.ToString() + "%";
    }

    private void UpdateWeaponPhysDamage(int physWeaponDamage)
    {
        _currentWeaponPhysDamage = physWeaponDamage;
        UpdateAllMainStats();
    }

    private void UpdateWeaponMageDamage(int mageWeaponDamage)
    {
        _currentWeaponMageDamage = mageWeaponDamage;
        UpdateAllMainStats();
    }

    private void UpdateWeaponPhysCritChance(float physChance)
    {
        _currentWeaponPhysCritChance = physChance;
        UpdateAllMainStats();
    }

    private void UpdateWeaponMageCritChance(float magChance)
    {
        _currentWeaponMageCritChance = magChance;
        UpdateAllMainStats();
    }

    private void OnDisable()
    {
        CustomEvents.OnUpdateAllStats -= UpdateAllMainStats;
        CustomEvents.OnUpdateWeaponPhysDamage -= UpdateWeaponPhysDamage;
        CustomEvents.OnUpdateWeaponMageDamage -= UpdateWeaponMageDamage;
        CustomEvents.OnUpdateWeaponPhysCritChance -= UpdateWeaponPhysCritChance;
        CustomEvents.OnUpdateWeaponMageCritChance -= UpdateWeaponMageCritChance;
    }
}

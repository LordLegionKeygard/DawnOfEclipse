using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsFormulesCalculator : MonoBehaviour
{
    [Header("Constitution")]
    [SerializeField] private HealthControl _healthControl;
    [SerializeField] private CharacterBaseHPInformation _baseHPInfo;


    [Header("Mind")]

    [SerializeField] private ManaControl _manaControl;
    [SerializeField] private CharacterBaseMPInformation _cbaseMPInfo;


    [Header("Vigor")]

    [SerializeField] private StaminaControl _staminaControl;
    [SerializeField] private CharacterBaseStaminaInformation _baseStaminaInfo;

    [Header("Dexterity")]
    [SerializeField] private PlayerMovement _playerMovement;


    [Header("Other")]

    [SerializeField] private PotionsControl _potionControl;


    private void OnEnable()
    {
        CustomEvents.OnCalculateAllStats += CalculateAll;
        CalculateAll(false);
    }

    private void CalculateAll(bool isBuff)
    {
        CalculateDexterity();
        CalculateConstitution(isBuff);
        CalculateEndurance(isBuff);
        CalculateMind(isBuff);
        CustomEvents.FireUpdateAllStats();
    }

    private void CalculateDexterity()
    {
        _playerMovement.DefaultSpeed = 3.95f + (0.01f * CharacterStats.Dexterity);
        switch (CharacterInformation.Class)
        {
            case 0:
                _staminaControl.MaxStamina = (int)((float)_baseStaminaInfo.Class[ExperienceControl.CurrentLevel].Fighter * (1 + 0.02f * CharacterStats.Endurance));
                break;
            case 1:
                _staminaControl.MaxStamina = (int)((float)_baseStaminaInfo.Class[ExperienceControl.CurrentLevel].Mage * (1 + 0.02f * CharacterStats.Endurance));
                break;
        }
        _playerMovement.CalculateSpeed();
    }
    private void CalculateConstitution(bool isBuff)
    {
        switch (CharacterInformation.Class)
        {
            case 0:
                _healthControl.MaxHealth = (int)((float)_baseHPInfo.Class[ExperienceControl.CurrentLevel].Fighter * (1 + 0.04f * CharacterStats.Constitution));
                break;
            case 1:
                _healthControl.MaxHealth = (int)((float)_baseHPInfo.Class[ExperienceControl.CurrentLevel].Mage * (1 + 0.04f * CharacterStats.Constitution));
                break;
        }
        _healthControl.CalculateHealth(isBuff);
        _potionControl.CalculatePotions();
    }
    private void CalculateEndurance(bool isBuff)
    {
        switch (CharacterInformation.Class)
        {
            case 0:
                _staminaControl.MaxStamina = (int)((float)_baseStaminaInfo.Class[ExperienceControl.CurrentLevel].Fighter * (1 + 0.02f * CharacterStats.Endurance));
                break;
            case 1:
                _staminaControl.MaxStamina = (int)((float)_baseStaminaInfo.Class[ExperienceControl.CurrentLevel].Mage * (1 + 0.02f * CharacterStats.Endurance));
                break;
        }
        _staminaControl.CalculateStamina(isBuff);
    }

    private void CalculateMind(bool isBuff)
    {
        switch (CharacterInformation.Class)
        {
            case 0:
                _manaControl.MaxMana = (int)((float)_cbaseMPInfo.Class[ExperienceControl.CurrentLevel].Fighter * (1 + 0.04f * CharacterStats.Mind));
                break;
            case 1:
                _manaControl.MaxMana = (int)((float)_cbaseMPInfo.Class[ExperienceControl.CurrentLevel].Mage * (1 + 0.04f * CharacterStats.Mind));
                break;
        }
        _manaControl.CalculateMana(isBuff);
    }


    private void OnDisable()
    {
        CustomEvents.OnCalculateAllStats -= CalculateAll;
    }
}

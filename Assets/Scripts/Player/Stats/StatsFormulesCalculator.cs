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
    }

    private void Awake()
    {
        CalculateAll();
    }

    private void CalculateAll()
    {
        CalculateStrength();
        CalculateDexterity();
        CalculateConstitution();
        CalculateVigor();
        CalculateIntelligence();
        CalculateWisdom();
        CalculateMind();
        CustomEvents.FireUpdateAllStats();
    }

    private void CalculateStrength()
    {
        
    }
    private void CalculateDexterity()
    {
        switch (CharacterInformation.Class)
        {
            case 0:
                _playerMovement.DefaultSpeed = 4.95f + (0.01f * CharacterStats.Dexterity);
                break;
            case 1:
                _staminaControl.MaxStamina = (int)((float)_baseStaminaInfo.Class[ExperienceControl.CurrentLevel].Mage * (1 + 0.02f * CharacterStats.Vigor));
                break;
        }
        _playerMovement.CalculateSpeed();
    }
    private void CalculateConstitution()
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
        _healthControl.CalculateHealth();
        _potionControl.CalculateHealFromPotion();
    }
    private void CalculateVigor()
    {
        switch (CharacterInformation.Class)
        {
            case 0:
                _staminaControl.MaxStamina = (int)((float)_baseStaminaInfo.Class[ExperienceControl.CurrentLevel].Fighter * (1 + 0.02f * CharacterStats.Vigor));
                break;
            case 1:
                _staminaControl.MaxStamina = (int)((float)_baseStaminaInfo.Class[ExperienceControl.CurrentLevel].Mage * (1 + 0.02f * CharacterStats.Vigor));
                break;
        }
        _staminaControl.CalculateStamina();
    }
    private void CalculateIntelligence()
    {

    }
    private void CalculateWisdom()
    {

    }
    private void CalculateMind()
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
        _manaControl.CalculateMana();
    }


    private void OnDisable()
    {
        CustomEvents.OnCalculateAllStats -= CalculateAll;
    }
}

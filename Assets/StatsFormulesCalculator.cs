using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsFormulesCalculator : MonoBehaviour
{
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private ExperienceControl _experienceControl;


    [Header("Constitution")]
    [SerializeField] private HealthControl _healthControl;

    [SerializeField] private ConstitutionModifier _constututionModifier;
    [SerializeField] private CharacterBaseHPInformation _characterBaseHPInformation;


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
    }

    private void CalculateStrength()
    {

    }
    private void CalculateDexterity()
    {

    }
    private void CalculateConstitution()
    {
        switch (CharacterInformation.Class)
        {
            case 0:
                _healthControl.MaxHealth = _characterBaseHPInformation.Class[_experienceControl.CurrentLevel].FighterBaseHealth * (int)_constututionModifier.ConModifier[_characterStats.Constitution];
                break;
            case 1:
                _healthControl.MaxHealth = _characterBaseHPInformation.Class[_experienceControl.CurrentLevel].MageBaseHealth;
                break;
        }

        // HP = ((Class Base HP*CON Modifier)
    }
    private void CalculateVigor()
    {

    }
    private void CalculateIntelligence()
    {

    }
    private void CalculateWisdom()
    {

    }
    private void CalculateMind()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    [SerializeField] private ArmorControl _armorControl;
    [SerializeField] private MagicArmorControl _magicArmorControl;

    private void Start()
    {
        CustomEvents.OnBuff += ChangeCharacterStats;
    }

    private void ChangeCharacterStats(int statsNumber, int amount)
    {
        switch (statsNumber)
        {
            case 0:
                CharacterStats.Strength += amount;
                break;
            case 1:
                CharacterStats.Dexterity += amount;
                break;
            case 2:
                CharacterStats.Constitution += amount;
                break;
            case 3:
                CharacterStats.Endurance += amount;
                break;
            case 4:
                CharacterStats.Intelligence += amount;
                break;
            case 5:
                CharacterStats.Wisdom += amount;
                break;
            case 6:
                CharacterStats.Mind += amount;
                break;
        }
        CustomEvents.FireCalculateAllStats(true);

        if(amount < 0)
        {
            CustomEvents.FireReturnPlayerStats(statsNumber);
        } 
    }

    private void OnDestroy()
    {
        CustomEvents.OnBuff -= ChangeCharacterStats;
    }
}

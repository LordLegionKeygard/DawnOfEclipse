using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    [SerializeField] private ArmorControl _armorControl;
    [SerializeField] private MagicArmorControl _magicArmorControl;

    private void Start()
    {
        CustomEvents.OnStatBuff += ChangeCharacterStats;
    }

    private void ChangeCharacterStats(int statsNumber, int amount)
    {
        switch (statsNumber)
        {
            case -1:
                //nothing
                break;
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

        if (amount < 0)
        {
            CustomEvents.FireReturnPlayerStats(statsNumber);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnStatBuff -= ChangeCharacterStats;
    }

    //Strength StatBuff = 0
    //Dexterity StatBuff = 1
    //Constitution StatBuff = 2
    //Endurance StatBuff = 3
    //Intelligence StatBuff = 4
    //Wisdom StatBuff = 5
    //Mind StatBuff = 6
    //Dark MagicBuff = 7
    //Fire MagicBuff = 8
    //Ice MagicBuff = 9
    //Light MagicBuff = 10
    //Nature MagicBuff = 11
    //Storm MagicBuff = 12
    //Arcane MagicBuff = 13
    //Blood MagicBuff = 14
}

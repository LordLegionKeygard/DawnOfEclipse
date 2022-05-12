using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public static int Strength;
    public static int Dexterity;
    public static int Constitution;
    public static int Endurance;
    public static int Intelligence;
    public static int Wisdom;
    public static int Mind;

    [SerializeField] private CharacterStatsInfo _characterStatsInfo;

    private void Awake()
    {
        Strength = _characterStatsInfo.intArray[CharacterInformation.Race].Strength[CharacterInformation.Class];
        Dexterity = _characterStatsInfo.intArray[CharacterInformation.Race].Dexterity[CharacterInformation.Class];
        Constitution = _characterStatsInfo.intArray[CharacterInformation.Race].Constitution[CharacterInformation.Class];
        Endurance = _characterStatsInfo.intArray[CharacterInformation.Race].Endurance[CharacterInformation.Class];
        Intelligence = _characterStatsInfo.intArray[CharacterInformation.Race].Intelligence[CharacterInformation.Class];
        Wisdom = _characterStatsInfo.intArray[CharacterInformation.Race].Wisdom[CharacterInformation.Class];
        Mind = _characterStatsInfo.intArray[CharacterInformation.Race].Mind[CharacterInformation.Class];
    }

    // STR (STRENGTH)
    // affects P.Atk.

    // END (ENDURANCE) +
    // affects maximum Stamina
    // affects Stamina recovery speed

    // CON (CONSTITUTION) +
    // affects maximum Health
    // affects Health recovery speed
    // affects underwater breath gauge
    // affects shock (stun) resistance
    // affects bleeding resistance.

    // DEX (DEXTERITY)
    // affects Critical Chance and Speed

    // INT (INTELLIGENCE)
    // affects M.Atk. 

    // WIT (WISDOM)
    // affects chance of magic critical hits
    // affects resistance to Hold (aka Roots) and Sleep

    // MIN (MIND) +
    // affects maximum MP
    // affects MP recovery speed
    // affects poison resistance
    // affects curse resistance
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public static int Strength;
    public static int Dexterity;
    public static int Constitution;
    public static int Vigor;
    public static int Intelligence;
    public static int Wisdom;
    public static int Mind;

    private void Awake()
    {
        Strength = CharacterInformation.StrengthS;
        Dexterity = CharacterInformation.DexterityS;
        Constitution = CharacterInformation.ConstitutionS;
        Vigor = CharacterInformation.VigorS;
        Intelligence = CharacterInformation.IntelligenceS;
        Wisdom = CharacterInformation.WisdomS;
        Mind = CharacterInformation.MindS;
    }

    // STR (STRENGTH)
    // affects P.Atk.

    // VIG (VIGOR) +
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

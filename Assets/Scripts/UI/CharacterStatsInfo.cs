using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllCharacterStatsInfo", menuName = "Info/CharacterStatsInfo")]
public class CharacterStatsInfo : ScriptableObject
{
    public AllStats[] intArray; //total 14
}

[System.Serializable]
public class AllStats
{
    public string Race;
    public int[] Strength;
    public int[] Dexterity;
    public int[] Constitution;
    public int[] Endurance;
    public int[] Intelligence;
    public int[] Wisdom;
    public int[] Mind;
    public int[] Luck;
}

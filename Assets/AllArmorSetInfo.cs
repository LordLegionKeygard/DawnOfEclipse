using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllArmorSetInfo", menuName = "Info/Armor")]
public class AllArmorSetInfo : ScriptableObject
{
    public ArmorSetEnum ArmorSetEnum;
    public IntArray[] intArray;
}

public enum ArmorSetEnum
{
    Savage = 0,
    Wolf = 1,
    Hunter = 2,
    Elite = 3,
    Monk = 4,
    Manticore = 5,
    PlagueDoctor = 6,
    Fanatic = 7,
    Destiny = 8,
    Mithril = 9,
    Archer = 10,
    Thorns = 11,
    PlatedLeather = 12,
    Bandit = 13,
    Ancient = 14,
    Legionary = 15,
    Horned = 16,
    SandGolem = 17,
    DarkElven = 18,
    BlackRider = 19,
    Carapace = 20,
    Demon = 21,
    Memorable = 22,
    FullPlate = 23,
    Lion = 24,
    Desert = 25,
    Wanderer = 26,
    Reinforced = 27,
    Swamp = 28,
    Pharaoh = 29
}

[System.Serializable]
public class IntArray
{
    public string[] HelmetInfo;
    public string[] ShouldersInfo;
    public string[] TorsoInfo;
    public string[] ForearmsInfo;
    public string[] ElbowsInfo;
    public string[] BracersInfo;
    public string[] GlovesInfo;
    public string[] HipsInfo;
    public string[] KneesInfo;
    public string[] BootsInfo;
    public string[] ThreePiecesInfo;
    public string[] SixPiecesInfo;
    public string[] EightPiecesInfo;
    public string[] TenPiecesInfo;
}
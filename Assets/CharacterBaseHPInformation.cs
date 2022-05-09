using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseHPInfo", menuName = "Info/BaseHP")]
public class CharacterBaseHPInformation : ScriptableObject
{
    public Class[] Class;
}

[System.Serializable]
public class Class
{
    public int FighterBaseHealth;
    public int MageBaseHealth;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCreationStatsChanger : MonoBehaviour
{
    [SerializeField] private CharacterStatsInfo _characterStatsInfo;
    [SerializeField] private TextMeshProUGUI[] _allStatsText;

    public void ChangeStats(int race, int charClass)
    {
        _allStatsText[0].text = _characterStatsInfo.intArray[race].Strength[charClass].ToString();
        _allStatsText[1].text = _characterStatsInfo.intArray[race].Dexterity[charClass].ToString();
        _allStatsText[2].text = _characterStatsInfo.intArray[race].Constitution[charClass].ToString();
        _allStatsText[3].text = _characterStatsInfo.intArray[race].Endurance[charClass].ToString();
        _allStatsText[4].text = _characterStatsInfo.intArray[race].Intelligence[charClass].ToString();
        _allStatsText[5].text = _characterStatsInfo.intArray[race].Wisdom[charClass].ToString();
        _allStatsText[6].text = _characterStatsInfo.intArray[race].Mind[charClass].ToString();
        _allStatsText[7].text = _characterStatsInfo.intArray[race].Luck[charClass].ToString();
    }
}

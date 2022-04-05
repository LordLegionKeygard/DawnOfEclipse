using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _inventoryText;
    [SerializeField] private TextMeshProUGUI[] _characterStatsText;

    private void Start()
    {
        _inventoryText[0].text = Language.TextStatic[1];
        _inventoryText[1].text = Language.TextStatic[7];
        _characterStatsText[0].text = Language.TextStatic[2];
        _characterStatsText[1].text = Language.TextStatic[3];
        _characterStatsText[2].text = Language.TextStatic[4];
        _characterStatsText[3].text = Language.TextStatic[5];
        _characterStatsText[4].text = Language.TextStatic[6];
    }
}

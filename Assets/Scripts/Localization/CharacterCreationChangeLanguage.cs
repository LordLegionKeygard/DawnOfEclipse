using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCreationChangeLanguage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _satyrText;
    [SerializeField] private TextMeshProUGUI[] _mushroomText;

    private void Start()
    {
        ChangeSatyrText();
        ChangeMushroomText();
    }

    private void ChangeSatyrText()
    {
        _satyrText[0].text = Language.TextStatic[9];
        _satyrText[1].text = Language.TextStatic[10];
        _satyrText[2].text = Language.TextStatic[11];
        _satyrText[3].text = Language.TextStatic[12];
        _satyrText[4].text = Language.TextStatic[13];
        _satyrText[5].text = Language.TextStatic[14];
        _satyrText[6].text = Language.TextStatic[15];
        _satyrText[7].text = Language.TextStatic[16];
        _satyrText[8].text = Language.TextStatic[17];
        _satyrText[9].text = Language.TextStatic[21];
        _satyrText[10].text = Language.TextStatic[19];
        _satyrText[11].text = Language.TextStatic[18];
        _satyrText[12].text = Language.TextStatic[25];
    }

    private void ChangeMushroomText()
    {
        _mushroomText[0].text = Language.TextStatic[9];
        _mushroomText[1].text = Language.TextStatic[22];
        _mushroomText[2].text = Language.TextStatic[13];
        _mushroomText[3].text = Language.TextStatic[14];
        _mushroomText[4].text = Language.TextStatic[24];
        _mushroomText[5].text = Language.TextStatic[26];
        _mushroomText[6].text = Language.TextStatic[23];
        _mushroomText[7].text = Language.TextStatic[25];
    }
}

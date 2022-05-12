using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCreationChangeLanguage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _satyrText;
    [SerializeField] private TextMeshProUGUI[] _mushroomText;
    [SerializeField] private TextMeshProUGUI[] _statsText;

    private void Start()
    {
        ChangeSatyrText();
        ChangeMushroomText();
        ChangeStatsText();
    }

    private void ChangeStatsText()
    {
        _statsText[0].text = Language.TextStatic[29];
        _statsText[1].text = Language.TextStatic[30];
        _statsText[2].text = Language.TextStatic[31];
        _statsText[3].text = Language.TextStatic[32];
        _statsText[4].text = Language.TextStatic[33];
        _statsText[5].text = Language.TextStatic[34];
        _statsText[6].text = Language.TextStatic[35];
        _statsText[7].text = Language.TextStatic[36];

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
        _satyrText[13].text = Language.TextStatic[27];
        _satyrText[14].text = Language.TextStatic[37];
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
        _mushroomText[8].text = Language.TextStatic[27];
        _mushroomText[9].text = Language.TextStatic[37];
    }

}

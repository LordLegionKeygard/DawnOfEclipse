using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 0 - English						en
// 1 - Russian						ru

public class Language : MonoBehaviour
{
    public static int LanguageNumber = 0;
    private string[,] _textPreviesStatic = new string[100, 2];
    public static string[] TextStatic = new string[100];
    private string _langChange;

    private void Awake()
    {
        SetLanguage();
    }

    public void SetLanguage()
    {
        _textPreviesStatic[1, 0] = "Moon";
        _textPreviesStatic[1, 1] = "Лун";

        _textPreviesStatic[2, 0] = "P. Def.";
        _textPreviesStatic[2, 1] = "Физ. Защ.";

        _textPreviesStatic[3, 0] = "M. Def.";
        _textPreviesStatic[3, 1] = "Маг. Защ.";

        _textPreviesStatic[4, 0] = "P. Atk.";
        _textPreviesStatic[4, 1] = "Физ. Атк.";

        _textPreviesStatic[5, 0] = "M. Atk.";
        _textPreviesStatic[5, 1] = "Маг. Атк.";

        _textPreviesStatic[6, 0] = "Character Stats";
        _textPreviesStatic[6, 1] = "Характеристики";

        _textPreviesStatic[7, 0] = "Inventory";
        _textPreviesStatic[7, 1] = "Инвентарь";

        _textPreviesStatic[8, 0] = "<Set Effect>";
        _textPreviesStatic[8, 1] = "<Эффект Комплекта>";

        // _langChange = PlayerPrefs.GetString("Language");

        // if (_langChange != "")
        // {
        //     LanguageNumber = int.Parse(_langChange);
        // }
        // else
        // {

        //     if (Application.systemLanguage == SystemLanguage.Russian)
        //     {
        //         LanguageNumber = 1;
        //     }
        //     else
        //     {
        //         LanguageNumber = 0;
        //     }

        //     PlayerPrefs.SetString("Language", LanguageNumber.ToString());
        //     PlayerPrefs.Save();
        // }

        for (int x = 0; x < 99; x++) { TextStatic[x] = _textPreviesStatic[x, LanguageNumber]; }
    }

    // public static void ChangeLanguage(int _language)
    // {
    //     LanguageNumber = _language;

    //     PlayerPrefs.SetString("Language", LanguageNumber.ToString());
    //     PlayerPrefs.Save();

    //     for (int x = 0; x < 99; x++) { Menu_text[x] = Menu_text_previes[x, _language]; }
    // }
}

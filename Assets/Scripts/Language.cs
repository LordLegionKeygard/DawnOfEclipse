using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 0 - English						en
// 1 - Russian						ru

public class Language : MonoBehaviour
{
    public static int LanguageNumber = 1;
    public static string[,] TextPreviesStatic = new string[100, 2];
    public static string[] TextStatic = new string[100];
    private string _langChange;

    private void Awake()
    {
        SetLanguage();
    }

    public void SetLanguage()
    {
        TextPreviesStatic[1, 0] = "Moon";
        TextPreviesStatic[1, 1] = "Лун";

        TextPreviesStatic[2, 0] = "P. Def.";
        TextPreviesStatic[2, 1] = "Физ. Защ.";

        TextPreviesStatic[3, 0] = "M. Def.";
        TextPreviesStatic[3, 1] = "Маг. Защ.";

        TextPreviesStatic[4, 0] = "P. Atk.";
        TextPreviesStatic[4, 1] = "Физ. Атк.";

        TextPreviesStatic[5, 0] = "M. Atk.";
        TextPreviesStatic[5, 1] = "Маг. Атк.";

        TextPreviesStatic[6, 0] = "Character Stats";
        TextPreviesStatic[6, 1] = "Характеристики";

        TextPreviesStatic[7, 0] = "Inventory";
        TextPreviesStatic[7, 1] = "Инвентарь";

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

        for (int x = 0; x < 99; x++) { TextStatic[x] = TextPreviesStatic[x, LanguageNumber]; }
    }

    // public static void ChangeLanguage(int _language)
    // {
    //     LanguageNumber = _language;

    //     PlayerPrefs.SetString("Language", LanguageNumber.ToString());
    //     PlayerPrefs.Save();

    //     for (int x = 0; x < 99; x++) { Menu_text[x] = Menu_text_previes[x, _language]; }
    // }
}

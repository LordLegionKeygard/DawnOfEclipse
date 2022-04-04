using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 0 - English						en
// 1 - Russian						ru

public class Language : MonoBehaviour
{
    public static int LanguageNumber = 1;
    public static string[,] Menu_text_previes = new string[100, 2];
    public static string[] Menu_text = new string[100];
    private string _langChange;

    private void Awake()
    {
        SetLanguage();
    }

    public void SetLanguage()
    {
        Menu_text_previes[1, 0] = "Moon";
        Menu_text_previes[1, 1] = "Лун";

        Menu_text_previes[2, 0] = "P. Def.";
        Menu_text_previes[2, 1] = "Физ. Защ.";

        Menu_text_previes[3, 0] = "M. Def.";
        Menu_text_previes[3, 1] = "Маг. Защ.";

        Menu_text_previes[4, 0] = "P. Atk.";
        Menu_text_previes[4, 1] = "Физ. Атк.";

        Menu_text_previes[5, 0] = "M. Atk.";
        Menu_text_previes[5, 1] = "Маг. Атк.";

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

        for (int x = 0; x < 99; x++) { Menu_text[x] = Menu_text_previes[x, LanguageNumber]; }
    }

    // public static void ChangeLanguage(int _language)
    // {
    //     LanguageNumber = _language;

    //     PlayerPrefs.SetString("Language", LanguageNumber.ToString());
    //     PlayerPrefs.Save();

    //     for (int x = 0; x < 99; x++) { Menu_text[x] = Menu_text_previes[x, _language]; }
    // }
}

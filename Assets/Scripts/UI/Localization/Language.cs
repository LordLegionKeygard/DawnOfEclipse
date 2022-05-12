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

        _textPreviesStatic[9, 0] = "Race";
        _textPreviesStatic[9, 1] = "Раса";

        _textPreviesStatic[10, 0] = "Gender";
        _textPreviesStatic[10, 1] = "Пол";

        _textPreviesStatic[11, 0] = "Hairstyle";
        _textPreviesStatic[11, 1] = "Прическа";

        _textPreviesStatic[12, 0] = "Hair Color";
        _textPreviesStatic[12, 1] = "Цвет Волос";

        _textPreviesStatic[13, 0] = "Skin Color";
        _textPreviesStatic[13, 1] = "Цвет Кожи";

        _textPreviesStatic[14, 0] = "Eye Color";
        _textPreviesStatic[14, 1] = "Цвет Глаз";

        _textPreviesStatic[15, 0] = "Mask";
        _textPreviesStatic[15, 1] = "Маска";

        _textPreviesStatic[16, 0] = "Horns";
        _textPreviesStatic[16, 1] = "Рога";

        _textPreviesStatic[17, 0] = "           - forest deities, fertility demons, cheerful goat-footed creatures. The satyr is lazy and dizzy, he spends his time drinking and hunting nymphs.";
        _textPreviesStatic[17, 1] = "              - лесные божества, демоны плодородия, жизнерадостные козлоногие существа. Сатир ленив и распутен, он проводит время в пьянстве и охоте за нимфами.";

        _textPreviesStatic[18, 0] = "Satyr";
        _textPreviesStatic[18, 1] = "Сатир";

        _textPreviesStatic[19, 0] = "Male";
        _textPreviesStatic[19, 1] = "Мужчина";

        _textPreviesStatic[20, 0] = "Female";
        _textPreviesStatic[20, 1] = "Женщина";

        _textPreviesStatic[21, 0] = "Satyrs";
        _textPreviesStatic[21, 1] = "Сатиры";

        _textPreviesStatic[22, 0] = "Head";
        _textPreviesStatic[22, 1] = "Голова";

        _textPreviesStatic[23, 0] = "Mushroom";
        _textPreviesStatic[23, 1] = "Гриб";

        _textPreviesStatic[24, 0] = "Mushrooms";
        _textPreviesStatic[24, 1] = "Грибы";

        _textPreviesStatic[25, 0] = "Create";
        _textPreviesStatic[25, 1] = "Создать";

        _textPreviesStatic[26, 0] = "                    - no one knows where they came from, but some of them can wake up from eternal sleep";
        _textPreviesStatic[26, 1] = "            - никто не знает, откуда они появились. Говорят они проснулись от вечного сна.";

        _textPreviesStatic[27, 0] = "Fighter";
        _textPreviesStatic[27, 1] = "Воитель";

        _textPreviesStatic[28, 0] = "Mystic";
        _textPreviesStatic[28, 1] = "Мистик";

        _textPreviesStatic[29, 0] = "Strength";
        _textPreviesStatic[29, 1] = "Сила";

        _textPreviesStatic[30, 0] = "Dexterity";
        _textPreviesStatic[30, 1] = "Ловкость";

        _textPreviesStatic[31, 0] = "Constitution";
        _textPreviesStatic[31, 1] = "Телосложение";

        _textPreviesStatic[32, 0] = "Endurance";
        _textPreviesStatic[32, 1] = "Выносливость";

        _textPreviesStatic[33, 0] = "Intelligence";
        _textPreviesStatic[33, 1] = "Интелект";

        _textPreviesStatic[34, 0] = "Wisdom";
        _textPreviesStatic[34, 1] = "Мудрость";

        _textPreviesStatic[35, 0] = "Mind";
        _textPreviesStatic[35, 1] = "Дух";

        _textPreviesStatic[36, 0] = "Luck";
        _textPreviesStatic[36, 1] = "Удача";

        _textPreviesStatic[37, 0] = "Class";
        _textPreviesStatic[37, 1] = "Класс";
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

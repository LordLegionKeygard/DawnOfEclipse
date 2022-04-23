using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCreationCharacteristicsChange : MonoBehaviour
{
    [SerializeField] private int _race;
    [SerializeField] private TextMeshProUGUI _genderText;
    [SerializeField] private int _hairStyle;
    [SerializeField] private int _hairColor;
    [SerializeField] private int _skinColor;
    [SerializeField] private int _eyeColor;
    [SerializeField] private int _mask;
    [SerializeField] private int _horns;
    [SerializeField] private Button[] _buttons;

    [Header ("Objects")]

    [SerializeField] private GameObject[] _genders;


    public void ChangeGender(string turn)
    {
        switch (turn)
        {
            case "Right":
                _genderText.text = "Female";
                _buttons[0].interactable = false;
                _buttons[1].interactable = true;
                _genders[0].SetActive(false);
                _genders[1].SetActive(true);
                break;
            case "Left":
                _genderText.text = "Male";
                _buttons[0].interactable = true;
                _buttons[1].interactable = false;
                _genders[0].SetActive(true);
                _genders[1].SetActive(false);
                break;
        }
    }
}

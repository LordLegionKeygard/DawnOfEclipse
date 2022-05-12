using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MushroomCreation : MonoBehaviour
{
    [Header("Characteristicks")]

    [SerializeField] private int _class;
    [SerializeField] private int _gender = 2; //always
    [SerializeField] private int _cap = 1;
    [SerializeField] private int _skinColor = 1;
    [SerializeField] private int _eyeColor = 1;

    [Header("Objects")]
    [SerializeField] private GameObject[] _heads;

    [Header("Colors")]
    [SerializeField] private Color[] _skinColorPalletes;
    [SerializeField] private Color[] _eyeColorPalletes;

    [Header("Other")]
    [SerializeField] private CharacterCreationStatsChanger _characterCreationStatsChanger;
    [SerializeField] private Material _characterMaterial;
    [SerializeField] private TextMeshProUGUI[] _allText;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Image _loadingScreen;
    [SerializeField] private Sprite _screen;

    public void UpdateStats()
    {
        _characterCreationStatsChanger.ChangeStats(1, _class);
    }

    public void ChangeClass(string turn)
    {
        switch (turn)
        {
            case "Right":
                _allText[3].text = Language.TextStatic[28];
                _buttons[6].interactable = false;
                _buttons[7].interactable = true;
                _class = 1;
                UpdateStats();
                break;
            case "Left":
                _allText[3].text = Language.TextStatic[27];
                _buttons[6].interactable = true;
                _buttons[7].interactable = false;
                _class = 0;
                UpdateStats();
                break;
        }
    }

    public void ChangeSkinColor(string turn)
    {
        switch (turn)
        {
            case "Right":
                _skinColor++;
                if (_skinColor == _skinColorPalletes.Length - 1)
                    _buttons[0].interactable = false;
                break;

            case "Left":
                _skinColor--;
                if (_skinColor == 1)
                    _buttons[1].interactable = false;
                break;
        }
        _characterMaterial.SetColor("_Color_Skin", _skinColorPalletes[_skinColor]);
        _characterMaterial.SetColor("_Color_Stubble", _skinColorPalletes[_skinColor]);
        _allText[0].text = _skinColor.ToString();
        if (_skinColor != 1)
            _buttons[1].interactable = true;
        if (_skinColor != _skinColorPalletes.Length - 1)
            _buttons[0].interactable = true;
    }

    public void ChangeCap(string turn)
    {
        _heads[_cap].SetActive(false);
        switch (turn)
        {
            case "Right":
                _cap++;
                if (_cap == _heads.Length - 1)
                    _buttons[2].interactable = false;
                break;

            case "Left":
                _cap--;
                if (_cap == 1)
                    _buttons[3].interactable = false;
                break;
        }
        _heads[_cap].SetActive(true);
        _allText[1].text = _cap.ToString();
        if (_cap != 1)
            _buttons[3].interactable = true;
        if (_cap != _heads.Length - 1)
            _buttons[2].interactable = true;
    }

    public void ChangeEyeColor(string turn)
    {
        switch (turn)
        {
            case "Right":
                _eyeColor++;
                if (_eyeColor == _eyeColorPalletes.Length - 1)
                    _buttons[4].interactable = false;
                break;

            case "Left":
                _eyeColor--;
                if (_eyeColor == 1)
                    _buttons[5].interactable = false;
                break;
        }
        _characterMaterial.SetColor("_Color_Eyes", _eyeColorPalletes[_eyeColor]);
        _allText[2].text = _eyeColor.ToString();
        if (_eyeColor != 1)
            _buttons[5].interactable = true;
        if (_eyeColor != _eyeColorPalletes.Length - 1)
            _buttons[4].interactable = true;
    }

    public void CreateCharacter()
    {
        _loadingScreen.sprite = _screen;

        CharacterInformation.Class = _class;
        CharacterInformation.Race = 1;
        CharacterInformation.Gender = _gender;
        CharacterInformation.SkinColor = _skinColor;
        CharacterInformation.Cap = _cap;
        CharacterInformation.EyeColor = _eyeColor;

        CustomEvents.FireCharacterCreate();
    }

    private void OnDisable()
    {
        _characterMaterial.SetColor("_Color_Skin", _skinColorPalletes[1]);
        _characterMaterial.SetColor("_Color_Eyes", _eyeColorPalletes[1]);
    }
}

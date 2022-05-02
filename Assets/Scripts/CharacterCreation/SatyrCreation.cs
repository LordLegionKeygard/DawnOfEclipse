using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SatyrCreation : MonoBehaviour
{
    [Header("Characteristicks")]
    [SerializeField] private int _gender = 2;
    [SerializeField] private int _hairStyle = 1;
    [SerializeField] private int _hairColor = 1;
    [SerializeField] private int _skinColor = 1;
    [SerializeField] private int _eyeColor = 1;
    [SerializeField] private int _mask = 1;
    [SerializeField] private int _horn = 1;


    [Header("Objects")]
    [SerializeField] private GameObject[] _maleGenders;
    [SerializeField] private GameObject[] _femaleGenders;
    [SerializeField] private GameObject[] _hairStyles;
    [SerializeField] private MeshFilter[] _masks;
    [SerializeField] private MeshFilter[] _horns;


    [Header("Colors")]
    [SerializeField] private Color[] _hairColorPalletes;
    [SerializeField] private Color[] _skinColorPalletes;
    [SerializeField] private Color[] _eyeColorPalletes;

    [Header("Other")]

    [SerializeField] private MeshFilter _characterMask;
    [SerializeField] private MeshFilter _characterHorns;
    [SerializeField] private Material _characterMaterial;
    [SerializeField] private Material _maskEyesMaterial;
    [SerializeField] private TextMeshProUGUI[] _allText;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Image _loadingScreen;
    [SerializeField] private Sprite _screen;

    private void GendersToggle(GameObject[] gender, GameObject[] opposite, bool state)
    {
        foreach (var obj in gender) { obj.SetActive(state); }
        foreach (var oppo in opposite) { oppo.SetActive(!state); }
    }

    public void ChangeGender(string turn)
    {
        switch (turn)
        {
            case "Right":
                _allText[0].text = "Female";
                _buttons[0].interactable = false;
                _buttons[1].interactable = true;
                GendersToggle(_maleGenders, _femaleGenders, false);
                _gender = 0;
                break;
            case "Left":
                _allText[0].text = "Male";
                _buttons[0].interactable = true;
                _buttons[1].interactable = false;
                GendersToggle(_maleGenders, _femaleGenders, true);
                _gender = 2;
                break;
        }
    }

    public void ChangeHairStyle(string turn)
    {
        _hairStyles[_hairStyle].SetActive(false);
        switch (turn)
        {
            case "Right":
                _hairStyle++;
                if (_hairStyle == _hairStyles.Length - 1)
                    _buttons[2].interactable = false;
                break;

            case "Left":
                _hairStyle--;
                if (_hairStyle == 1)
                    _buttons[3].interactable = false;
                break;
        }
        _hairStyles[_hairStyle].SetActive(true);
        _allText[1].text = _hairStyle.ToString();
        if (_hairStyle != 1)
            _buttons[3].interactable = true;
        if (_hairStyle != _hairStyles.Length - 1)
            _buttons[2].interactable = true;
    }

    public void ChangeHairColor(string turn)
    {
        switch (turn)
        {
            case "Right":
                _hairColor++;
                if (_hairColor == _hairColorPalletes.Length - 1)
                    _buttons[4].interactable = false;
                break;

            case "Left":
                _hairColor--;
                if (_hairColor == 1)
                    _buttons[5].interactable = false;
                break;
        }
        _characterMaterial.SetColor("_Color_Hair", _hairColorPalletes[_hairColor]);
        _allText[2].text = _hairColor.ToString();
        if (_hairColor != 1)
            _buttons[5].interactable = true;
        if (_hairColor != _hairColorPalletes.Length - 1)
            _buttons[4].interactable = true;
    }

    public void ChangeSkinColor(string turn)
    {
        switch (turn)
        {
            case "Right":
                _skinColor++;
                if (_skinColor == _skinColorPalletes.Length - 1)
                    _buttons[6].interactable = false;
                break;

            case "Left":
                _skinColor--;
                if (_skinColor == 1)
                    _buttons[7].interactable = false;
                break;
        }
        _characterMaterial.SetColor("_Color_Skin", _skinColorPalletes[_skinColor]);
        _characterMaterial.SetColor("_Color_Stubble", _skinColorPalletes[_skinColor]);
        _allText[3].text = _skinColor.ToString();
        if (_skinColor != 1)
            _buttons[7].interactable = true;
        if (_skinColor != _skinColorPalletes.Length - 1)
            _buttons[6].interactable = true;
    }

    public void ChangeEyeColor(string turn)
    {
        switch (turn)
        {
            case "Right":
                _eyeColor++;
                if (_eyeColor == _eyeColorPalletes.Length - 1)
                    _buttons[8].interactable = false;
                break;

            case "Left":
                _eyeColor--;
                if (_eyeColor == 1)
                    _buttons[9].interactable = false;
                break;
        }
        _maskEyesMaterial.SetColor("_EmissionColor", _eyeColorPalletes[_eyeColor] * 10);
        _allText[4].text = _eyeColor.ToString();
        if (_eyeColor != 1)
            _buttons[9].interactable = true;
        if (_eyeColor != _eyeColorPalletes.Length - 1)
            _buttons[8].interactable = true;
    }

    public void ChangeMask(string turn)
    {
        switch (turn)
        {
            case "Right":
                _mask++;
                if (_mask == _masks.Length - 1)
                    _buttons[10].interactable = false;
                break;

            case "Left":
                _mask--;
                if (_mask == 1)
                    _buttons[11].interactable = false;
                break;
        }
        _characterMask.sharedMesh = _masks[_mask].sharedMesh;
        _allText[5].text = _mask.ToString();
        if (_mask != 1)
            _buttons[11].interactable = true;
        if (_mask != _masks.Length - 1)
            _buttons[10].interactable = true;
    }

    public void ChangeHorns(string turn)
    {
        switch (turn)
        {
            case "Right":
                _horn++;
                if (_horn == _horns.Length - 1)
                    _buttons[12].interactable = false;
                break;

            case "Left":
                _horn--;
                if (_horn == 1)
                    _buttons[13].interactable = false;
                break;
        }
        _characterHorns.sharedMesh = _horns[_horn].sharedMesh;
        _allText[6].text = _horn.ToString();
        if (_horn != 1)
            _buttons[13].interactable = true;
        if (_horn != _horns.Length - 1)
            _buttons[12].interactable = true;
    }

    public void CreateCharacter()
    {
        _loadingScreen.sprite = _screen;

        CharacterInformation.Race = 0;
        CharacterInformation.Gender = _gender;
        CharacterInformation.Hairstyle = _hairStyle;
        CharacterInformation.HairColor = _hairColor;
        CharacterInformation.SkinColor = _skinColor;
        CharacterInformation.EyeColor = _eyeColor;
        CharacterInformation.Mask = _mask;
        CharacterInformation.Horns = _horn;

        CustomEvents.FireCharacterCreate();
    }

    private void OnDisable()
    {
        _characterMaterial.SetColor("_Color_Hair", _hairColorPalletes[1]);
        _characterMaterial.SetColor("_Color_Skin", _skinColorPalletes[1]);
        _characterMaterial.SetColor("_Color_Stubble", _skinColorPalletes[1]);
        _maskEyesMaterial.SetColor("_EmissionColor", _eyeColorPalletes[1] * 10);
    }
}

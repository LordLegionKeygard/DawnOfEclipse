using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRaceChanger : MonoBehaviour
{
    [SerializeField] private Material _hair;
    [SerializeField] private Material _satyrSkin;
    [SerializeField] private Material _satyrMaleBody;
    [SerializeField] private Material _mushroomSkin;
    [SerializeField] private GameObject[] _heads;

    [Header("SatyrRace_____________________________________________")]

    [SerializeField] private GameObject[] _satyrParts;
    [SerializeField] private GameObject[] _satyrHairStyles;
    [SerializeField] private GameObject[] _satyrMasks;
    [SerializeField] private GameObject[] _satyrHorns;
    [SerializeField] private Color[] _hairSatyrColorPalletes;
    [SerializeField] private Color[] _skinSatyrColorPalletes;
    [SerializeField] private Color[] _eyeSatyrColorPalletes;
    [SerializeField] private Material _satyrMaskEyes;

    [Header("MushroomRace___________________________________________")]

    [SerializeField] private GameObject[] _mushroomParts;
    [SerializeField] private GameObject[] _mushroomHeads;
    [SerializeField] private Color[] _skinMushroomColorPalletes;
    [SerializeField] private Color[] _eyeMushroomColorPalletes;
    [SerializeField] private Material _mushroomRaceMaterial;
    [SerializeField] private PoisonDamageCollider[] _poisonDamageColliders;

    private void Start()
    {
        ChangeRace();
    }

    private void ChangeRace()
    {
        switch (CharacterInformation.Race)
        {
            case 0:
                _satyrParts[0].SetActive(true);

                _satyrHairStyles[CharacterInformation.Hairstyle].SetActive(true);
                _satyrMasks[CharacterInformation.Mask].SetActive(true);
                _satyrHorns[CharacterInformation.Horns].SetActive(true);
                _hair.color = _hairSatyrColorPalletes[CharacterInformation.HairColor];
                _satyrMaleBody.color = _skinSatyrColorPalletes[CharacterInformation.SkinColor];
                _satyrSkin.color = _skinSatyrColorPalletes[CharacterInformation.SkinColor];
                _satyrMaskEyes.SetColor("_EmissionColor", _eyeSatyrColorPalletes[CharacterInformation.EyeColor] * 10);

                if (CharacterInformation.Gender == 0)
                {
                    _satyrParts[1].SetActive(true);
                    _satyrParts[2].SetActive(true);
                    _heads[0].SetActive(true);
                }
                if (CharacterInformation.Gender == 2)
                {
                    _satyrParts[3].SetActive(true);
                    _satyrParts[4].SetActive(true);
                    _heads[1].SetActive(true);
                }
                break;
            case 1:
                _mushroomParts[0].SetActive(true);
                _mushroomParts[1].SetActive(true);
                _mushroomHeads[CharacterInformation.Cap].SetActive(true);
                _hair.SetColor("_Color_Skin", _skinMushroomColorPalletes[CharacterInformation.SkinColor]);
                _mushroomRaceMaterial.SetColor("_Color_Skin", _skinMushroomColorPalletes[CharacterInformation.SkinColor]);
                _mushroomRaceMaterial.SetColor("_Color_Eyes", _eyeMushroomColorPalletes[CharacterInformation.EyeColor]);

                foreach (var item in _poisonDamageColliders) { item.enabled = true; }
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRaceChanger : MonoBehaviour
{
    [SerializeField] private Material _hair;
    [SerializeField] private Material _satyrSkin;
    [SerializeField] private Material _satyrMaleBody;
    [SerializeField] private Material _mushroomSkin;

    [Header("SatyrRace_____________________________________________")]

    [SerializeField] private GameObject[] _satyrGender;
    [SerializeField] private GameObject _satyrTail;
    [SerializeField] private GameObject[] _satyrHairStyles;
    [SerializeField] private GameObject[] _satyrMasks;
    [SerializeField] private GameObject[] _satyrHorns;
    [SerializeField] private Color[] _hairSatyrColorPalletes;
    [SerializeField] private Color[] _skinSatyrColorPalletes;
    [SerializeField] private Color[] _eyeSatyrColorPalletes;
    [SerializeField] private Material _satyrMaskEyes;

    [Header("MushroomRace___________________________________________")]
    [SerializeField] private GameObject[] _mushroomCaps;
    [SerializeField] private Color[] _skinMushroomColorPalletes;
    [SerializeField] private Color[] _eyeMushroomColorPalletes;
    [SerializeField] private Material[] _mushroomRaceSkinMaterials;
    [SerializeField] private Material _mushroomEyeMaterial;
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
                _satyrTail.SetActive(true);

                _satyrHairStyles[CharacterInformation.Hairstyle].SetActive(true);
                _satyrMasks[CharacterInformation.Mask].SetActive(true);
                _satyrHorns[CharacterInformation.Horns].SetActive(true);
                _hair.color = _hairSatyrColorPalletes[CharacterInformation.HairColor];
                _satyrMaleBody.color = _skinSatyrColorPalletes[CharacterInformation.SkinColor];
                _satyrSkin.color = _skinSatyrColorPalletes[CharacterInformation.SkinColor];
                _satyrMaskEyes.SetColor("_EmissionColor", _eyeSatyrColorPalletes[CharacterInformation.EyeColor] * 10);

                _satyrGender[CharacterInformation.Gender].SetActive(true);

                break;
            case 1:
                foreach (var item in _mushroomRaceSkinMaterials) item.SetColor("_BaseMap", _skinMushroomColorPalletes[CharacterInformation.SkinColor]);
                _mushroomEyeMaterial.SetColor("_BaseMap", _eyeMushroomColorPalletes[CharacterInformation.EyeColor]);

                foreach (var item in _poisonDamageColliders) { item.enabled = true; }

                _mushroomCaps[CharacterInformation.Cap].SetActive(true);
                break;
        }
    }
}

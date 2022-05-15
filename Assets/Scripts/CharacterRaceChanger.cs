using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRaceChanger : MonoBehaviour
{
    [SerializeField] private Material _characterMaterial;
    [SerializeField] private GameObject[] _heads;
    [SerializeField] private GameObject[] _defaultLegs;

    [Header("SatyrRace_____________________________________________")]

    [SerializeField] private GameObject[] _satyrParts;
    [SerializeField] private GameObject[] _satyrHairStyles;
    [SerializeField] private MeshFilter[] _satyrMasks;
    [SerializeField] private MeshFilter[] _satyrHorns;
    [SerializeField] private MeshFilter _satyrMask;
    [SerializeField] private MeshFilter _satyrHorn;
    [SerializeField] private Color[] _hairSatyrColorPalletes;
    [SerializeField] private Color[] _skinSatyrColorPalletes;
    [SerializeField] private Color[] _eyeSatyrColorPalletes;
    [SerializeField] private Material _maskAndHornedMaterial;

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
                _satyrParts[1].SetActive(true);
                _satyrParts[2].SetActive(true);
                _defaultLegs[0].SetActive(false);
                _defaultLegs[1].SetActive(false);

                _satyrHairStyles[CharacterInformation.Hairstyle].SetActive(true);
                _satyrMask.sharedMesh = _satyrMasks[CharacterInformation.Mask].sharedMesh;
                _satyrHorn.sharedMesh = _satyrHorns[CharacterInformation.Horns].sharedMesh;
                _characterMaterial.SetColor("_Color_Hair", _hairSatyrColorPalletes[CharacterInformation.HairColor]);
                _characterMaterial.SetColor("_Color_Skin", _skinSatyrColorPalletes[CharacterInformation.SkinColor]);
                _characterMaterial.SetColor("_Color_Stubble", _skinSatyrColorPalletes[CharacterInformation.SkinColor]);
                _maskAndHornedMaterial.SetColor("_EmissionColor", _eyeSatyrColorPalletes[CharacterInformation.EyeColor] * 10);

                if (CharacterInformation.Gender == 0)
                {
                    _satyrParts[3].SetActive(true);
                    _satyrParts[4].SetActive(true);
                    _heads[0].SetActive(true);
                }
                if (CharacterInformation.Gender == 2)
                {
                    _satyrParts[5].SetActive(true);
                    _satyrParts[6].SetActive(true);
                    _heads[1].SetActive(true);
                }
                break;
            case 1:
                _mushroomParts[0].SetActive(true);
                _mushroomParts[1].SetActive(true);
                _mushroomHeads[CharacterInformation.Cap].SetActive(true);
                _characterMaterial.SetColor("_Color_Skin", _skinMushroomColorPalletes[CharacterInformation.SkinColor]);
                _mushroomRaceMaterial.SetColor("_Color_Skin", _skinMushroomColorPalletes[CharacterInformation.SkinColor]);
                _mushroomRaceMaterial.SetColor("_Color_Eyes", _eyeMushroomColorPalletes[CharacterInformation.EyeColor]);

                foreach (var item in _poisonDamageColliders) { item.enabled = true; }
                break;
        }
    }
}

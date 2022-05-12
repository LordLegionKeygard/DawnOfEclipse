using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterCreationRaceChanger : MonoBehaviour
{
    public int Race = 0; //satyr
    [SerializeField] private GameObject[] _terrainsAndCharacters;
    [SerializeField] private GameObject[] _racePanels;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private SatyrCreation _satyrCreation;
    [SerializeField] private MushroomCreation _mushroomCreation;

    public void ChangeRace(string turn)
    {
        _terrainsAndCharacters[Race].SetActive(false);
        _racePanels[Race].SetActive(false);
        switch (turn)
        {
            case "Right":
                Race++;
                if (Race == _racePanels.Length - 1)
                {
                    _buttons[_buttons.Length - 1].interactable = false;
                    _mushroomCreation.UpdateStats();
                }
                break;

            case "Left":
                Race--;
                if (Race == 0)
                {
                    _buttons[0].interactable = false;
                    _satyrCreation.UpdateStats();
                }
                break;
        }
        _terrainsAndCharacters[Race].SetActive(true);
        _racePanels[Race].SetActive(true);
    }
}

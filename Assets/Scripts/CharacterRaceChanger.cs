using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRaceChanger : MonoBehaviour
{
    [SerializeField] private Race _race;
    [SerializeField] private GameObject[] _defaultLegs;
    [SerializeField] private GameObject[] _forestRaceParts;


    private void Start()
    {
        ChangeRace();
    }

    private void ChangeRace()
    {
        switch (_race)
        {
            case 0:
                _defaultLegs[0].SetActive(false);
                _defaultLegs[1].SetActive(false);
                foreach (var item in _forestRaceParts)
                {
                    item.SetActive(true);
                }
                break;
        }
    }
}

[System.Serializable]

public enum Race
{
    Forest = 0,

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinwheel.Jupiter;

public class CharacterCreationChangeTime : MonoBehaviour
{
    [SerializeField] JDayNightCycle _jDayNightSicle;
    [SerializeField] private int _race;

    private void OnEnable()
    {
        ChangeTime(_race);
    }

    public void ChangeTime(int race)
    {
        switch (race)
        {

            case 0:
                _jDayNightSicle.Time = 20;
                break;
            case 1:
                _jDayNightSicle.Time = 13;
                break;
        }
    }
}

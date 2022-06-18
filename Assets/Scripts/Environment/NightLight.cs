using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinwheel.Jupiter;

public class NightLight : MonoBehaviour
{
    [SerializeField] private JDayNightCycle _jDayNightCycle;

    [SerializeField] private GameObject[] _light;

    private void FixedUpdate()
    {
        if (_jDayNightCycle.Time > 6 && _jDayNightCycle.Time < 18 && _light[0].activeInHierarchy)
        {
            foreach (var item in _light)
            {
                item.SetActive(false);
            }
        }
        else if ((_jDayNightCycle.Time < 6 || _jDayNightCycle.Time > 18) && !_light[0].activeInHierarchy)
        {
            foreach (var item in _light)
            {
                item.SetActive(true);
            }
        }
    }
}

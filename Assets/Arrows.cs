using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    [SerializeField] private Bow _bow;
    [SerializeField] private GameObject[] _allArrows;

    private void OnEnable()
    {
        CustomEvents.OnUseArrow += ChangeArrow;
    }

    private void ChangeArrow(int arrowNumber)
    {
        _bow.ArrowPrefab = _allArrows[arrowNumber];
    }

    private void OnDisable()
    {
        CustomEvents.OnUseArrow -= ChangeArrow;
    }
}

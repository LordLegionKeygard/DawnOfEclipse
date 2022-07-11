using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeArrowMesh : MonoBehaviour
{
    [SerializeField] private GameObject[] _arrows;

    private void Start()
    {
        CustomEvents.OnUseArrow += ChangeArrow;
    }

    private void ChangeArrow(int arrowNumber)
    {
        foreach (var item in _arrows) item.SetActive(false);
        _arrows[arrowNumber].SetActive(true);
    }

    private void OnDestroy()
    {
        CustomEvents.OnUseArrow -= ChangeArrow;
    }
}

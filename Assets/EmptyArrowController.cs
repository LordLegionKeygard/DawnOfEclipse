using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyArrowController : MonoBehaviour
{
    [SerializeField] private GameObject _arrowEmptyPrefab;
    private void OnEnable()
    {
        CustomEvents.OnTakeArrow += TakeArrow;
        CustomEvents.OnShootArrow += ShootArrow;
    }

    private void TakeArrow()
    {
        _arrowEmptyPrefab.SetActive(true);
    }

    private void ShootArrow(bool state)
    {
        _arrowEmptyPrefab.SetActive(!state);
    }

    private void OnDestroy()
    {
        CustomEvents.OnTakeArrow -= TakeArrow;
        CustomEvents.OnShootArrow -= ShootArrow;
    }
}

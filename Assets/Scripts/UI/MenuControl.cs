using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    [SerializeField] private GameObject _targetCircle;
    [SerializeField] private GameObject _allPanels;

    private void OnEnable()
    {
        CustomEvents.OnMenuToggle += MenuToggle;
    }

    private void MenuToggle()
    {
        if(_targetCircle.activeInHierarchy) return;
        _allPanels.SetActive(!_allPanels.activeSelf);
    }

    private void OnDisable()
    {
        CustomEvents.OnMenuToggle -= MenuToggle;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableInventory : MonoBehaviour
{
    [SerializeField] private GameObject[] _otherPanel;
    private void OnEnable()
    {
        foreach (var panel in _otherPanel)
            panel.SetActive(false);
    }

    private void OnDisable()
    {
        CustomEvents.FireTooltipToggle(false);
    }
}

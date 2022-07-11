using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _tooltip;

    private void OnEnable()
    {
        CustomEvents.OnTooltipToggle += ShowToggle;
    }

    public void ShowToggle(bool state, int toolTipNumer)
    {
        _tooltip[toolTipNumer].SetActive(state);
    }

    private void OnDisable()
    {
        CustomEvents.OnTooltipToggle -= ShowToggle;
    }
}

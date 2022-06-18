using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTriggerShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TraderShopSlot _traderShopSlot;
    [SerializeField] private TooltipSystem _tooltipSystem;

    private void Awake()
    {
        _traderShopSlot = GetComponent<TraderShopSlot>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_traderShopSlot != null && !_traderShopSlot.IsItemSelect) return;
        CustomEvents.FireUpdateSelectItemTransform();
        _tooltipSystem.ShowToggle(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltipSystem.ShowToggle(false);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTriggerShopSellSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private InventorySellSlot _inventorySellSlot;
    [SerializeField] private TooltipSystem _tooltipSystem;

    private void Awake()
    {
        _inventorySellSlot = GetComponent<InventorySellSlot>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_inventorySellSlot != null && !_inventorySellSlot.IsItemSelect) return;
        CustomEvents.FireUpdateSelectItemTransform();
        _tooltipSystem.ShowToggle(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltipSystem.ShowToggle(false);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private InventorySlot _inventorySlot;
    private EquipSlot _equipSlot;
    public bool IsEquipSlot;
    [SerializeField] private TooltipSystem _tooltipSystem;

    private void Awake()
    {
        if (!IsEquipSlot)
            _inventorySlot = GetComponent<InventorySlot>();
        else
            _equipSlot = GetComponent<EquipSlot>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_inventorySlot != null && !_inventorySlot.IsItemSelect) return;
        if (_equipSlot != null && !_equipSlot.IsItemSelect) return;
        CustomEvents.FireUpdateSelectItemTransform();
        _tooltipSystem.ShowToggle(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltipSystem.ShowToggle(false);
    }
}

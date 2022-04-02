using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotListener : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private InventorySlot _inventorySlot;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            CustomEvents.FireSelectItem(false);
            _inventorySlot.SelectSlot(true);
        }           
        else if (eventData.button == PointerEventData.InputButton.Right)
            _inventorySlot.UseItem();
    }
}

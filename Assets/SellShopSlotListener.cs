using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SellShopSlotListener : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private InventorySellSlot _inventorySellSlot;

    public void OnPointerUp(PointerEventData eventData)
    {
        CustomEvents.FireSelectItem(false);
        
        if (eventData.button == PointerEventData.InputButton.Left)
            _inventorySellSlot.SelectSlot(true);
        
        else if (eventData.button == PointerEventData.InputButton.Right)      
            _inventorySellSlot.SelectItem();
    }
}

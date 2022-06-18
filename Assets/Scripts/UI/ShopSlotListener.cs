using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ShopSlotListener : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private TraderShopSlot _traderShopSlot;

    public void OnPointerUp(PointerEventData eventData)
    {
        CustomEvents.FireSelectItem(false);
        
        if (eventData.button == PointerEventData.InputButton.Left)
            _traderShopSlot.SelectSlot(true);
        
        else if (eventData.button == PointerEventData.InputButton.Right)      
            _traderShopSlot.SelectItem();
    }
}

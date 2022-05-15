using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipSlotListener : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private EquipSlot _equipSlot;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            CustomEvents.FireSelectItem(false);
            _equipSlot.SelectSlot(true);
        }           
        else if (eventData.button == PointerEventData.InputButton.Right)
            _equipSlot.Unequip();
    }
}

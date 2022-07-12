using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DragAndDropSkill : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private SkillTreeButton _oldSlot;
    [SerializeField] private GameObject _parent;
    public RectTransform RectTransform;


    public void OnDrag(PointerEventData eventData)
    {
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        CustomEvents.FireTooltipToggle(false, 1);

        GetComponentInChildren<Image>().raycastTarget = false;

        transform.SetParent(transform.parent.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponentInChildren<Image>().raycastTarget = true;

        //Поставить DraggableObject обратно в свой старый слот
        transform.SetParent(_parent.transform);
        RectTransform.anchoredPosition = new Vector2(30, 30);
        //Если мышка отпущена над объектом по имени UIPanel, то...
        // if (eventData.pointerCurrentRaycast.gameObject.name == "Inventory")
        // {
        //     // Выброс объектов из инвентаря - Спавним префаб обекта перед персонажем
        //     // GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, player.position + Vector3.up + player.forward, Quaternion.identity);
        //     // Устанавливаем количество объектов такое какое было в слоте
        //     // itemObject.GetComponent<Item>().Amount = oldSlot._amount;
        //     // убираем значения InventorySlot
        //     NullifySlotData();
        // }
        // if (eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<InventorySlot>() != null)
        // {
        //     //Перемещаем данные из одного слота в другой
        //     ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<InventorySlot>());
        // }
    }
}

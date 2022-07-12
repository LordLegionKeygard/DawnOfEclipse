using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
/// IPointerDownHandler - Следит за нажатиями мышки по объекту на котором висит этот скрипт
/// IPointerUpHandler - Следит за отпусканием мышки по объекту на котором висит этот скрипт
/// IDragHandler - Следит за тем не водим ли мы нажатую мышку по объекту
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Item _emptyItem;
    public InventorySlot oldSlot;
    private Transform player;
    public RectTransform RectTransform;

    private void Start()
    {
        //ПОСТАВЬТЕ ТЭГ "PLAYER" НА ОБЪЕКТЕ ПЕРСОНАЖА!
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void OnDrag(PointerEventData eventData)
    {
        // Если слот пустой, то мы не выполняем то что ниже return;
        if (oldSlot.Item.name == "Empty_Item" || eventData.button == PointerEventData.InputButton.Right) return;
        Debug.Log("OnDrag");
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (oldSlot.Item.name == "Empty_Item" || eventData.button == PointerEventData.InputButton.Right) return;
        CustomEvents.FireTooltipToggle(false,0);
        CustomEvents.OnSelectItem(false);
        //Делаем картинку прозрачнее
        // GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        // Делаем так чтобы нажатия мышкой не игнорировали эту картинку
        GetComponentInChildren<Image>().raycastTarget = false;
        // Делаем наш DraggableObject ребенком InventoryPanel чтобы DraggableObject был над другими слотами инвенторя
        transform.SetParent(transform.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (oldSlot.Item.name == "Empty_Item" || eventData.button == PointerEventData.InputButton.Right) return;
        // Делаем картинку опять не прозрачной
        // GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        // И чтобы мышка опять могла ее засечь
        GetComponentInChildren<Image>().raycastTarget = true;

        //Поставить DraggableObject обратно в свой старый слот
        transform.SetParent(oldSlot.transform);
        RectTransform.anchoredPosition = new Vector2(0, 0);
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
        if (eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<InventorySlot>() != null)
        {
            //Перемещаем данные из одного слота в другой
            ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<InventorySlot>());
        }
    }
    private void NullifySlotData()
    {
        // убираем значения InventorySlot
        oldSlot.Item = _emptyItem;
        oldSlot._amount = 1;
        oldSlot.Icon.enabled = false;
    }
    private void ExchangeSlotData(InventorySlot newSlot)
    {
        CustomEvents.FireTooltipToggle(false,0);
        CustomEvents.OnSelectItem(false);
        // Временно храним данные newSlot в отдельных переменных
        Item item = newSlot.Item;
        int amount = newSlot._amount;
        Image iconGO = newSlot.Icon;
        // TMP_Text itemAmountText = newSlot.itemAmountText;

        // Заменяем значения newSlot на значения oldSlot
        newSlot.Item = oldSlot.Item;
        newSlot._amount = oldSlot._amount;
        if (oldSlot.Item.name != "Empty_Item")
        {
            oldSlot.Icon.enabled = false;
            newSlot.Icon.enabled = true;
            newSlot.Icon.sprite = oldSlot.Icon.sprite;
            newSlot._amountText.text = oldSlot._amount.ToString();
        }
        else
        {
            // newSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.Icon = null;
            newSlot.Icon.enabled = true;
            newSlot._amountText.enabled = false;
        }

        // newSlot.isEmpty = oldSlot.isEmpty;

        // Заменяем значения oldSlot на значения newSlot сохраненные в переменных
        oldSlot.Item = item;
        oldSlot._amount = amount;

        if (oldSlot.Item.name != "Empty_Item")
        {
            oldSlot.Icon.sprite = oldSlot.Item.icon;
            oldSlot.Icon.enabled = true;
            oldSlot._amountText.text = amount.ToString();
        }
        else
        {
            oldSlot.Icon.sprite = null;
            oldSlot.Icon.enabled = false;
        }

        if (oldSlot.Item.IsUsedItem)
            oldSlot._amountText.enabled = true;
        else
            oldSlot._amountText.enabled = false;
        if (newSlot.Item.IsUsedItem)
            newSlot._amountText.enabled = true;
        else
            newSlot._amountText.enabled = false;
    }
}

using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public bool isCursor;
    [SerializeField] private int _numberSlot;
    [SerializeField] private int _amount;
    [SerializeField] private Image icon;
    public TextMeshProUGUI _amountText;
    [SerializeField] private Button removeButton;
    [SerializeField] private GameObject ringFromBtn;
    public Item item;

    private void Update()
    {
        if (!isCursor) return;

        transform.position = Input.mousePosition;
    }

    public void AddItem(Item newItem, string name)
    {
        item = newItem;
        icon.sprite = item.icon;
        removeButton.interactable = true;
        if (newItem.name != "Empty_Item")
            icon.enabled = true;
        ringFromBtn.SetActive(true);
        if (item.maxStack > 1)
        {
            _amountText.enabled = true;
            if (newItem.name == name)
                _amount += item.amount;
            _amountText.text = _amount.ToString();
        }
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        ringFromBtn.SetActive(false);
        _amountText.enabled = false;
        _amount = 1;
    }
    public void OnRemoveButton()
    {
        Inventory.instance.RemoveItemFromInventoryList(item, _numberSlot);
        ClearSlot();
    }
    public void UseItem()
    {
        if (item.isUsedItem && PotionsControl.CanDrinkAnyPotions)
        {
            _amount--;
            _amountText.text = _amount.ToString();
            item.Use();
            switch (item.name)
            {
                case ("HealthPotion"):
                    {
                        CustomEvents.FireUsePotion(0);
                        break;
                    }
                case ("SpeedPotion"):
                    {
                        CustomEvents.FireUsePotion(1);
                        break;
                    }
                case ("ManaPotion"):
                    {
                        CustomEvents.FireUsePotion(2);
                        break;
                    }
            }
            if (_amount == 0)
            {
                OnRemoveButton();
            }
            return;
        }
        if (item.amount == 0)
        {
            item.Use();
            OnRemoveButton();
        }
    }
}

using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public bool IsCursor;
    [SerializeField] private int _numberSlot;
    [SerializeField] private int _amount;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Button _removeButton;
    [SerializeField] private GameObject _ringFromBtn;
    public Item Item;

    private void Update()
    {
        if (!IsCursor) return;

        transform.position = Input.mousePosition;
    }

    public void AddItem(Item newItem, string name)
    {
        Item = newItem;
        icon.sprite = Item.icon;
        _removeButton.interactable = true;
        if (newItem.name != "Empty_Item")
            icon.enabled = true;
        _ringFromBtn.SetActive(true);
        if (Item.IsStackable)
        {
            _amountText.enabled = true;
            if (newItem.name == name)
                _amount++;
            _amountText.text = _amount.ToString();
        }
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
        _removeButton.interactable = false;
        _ringFromBtn.SetActive(false);
        _amountText.enabled = false;
        _amount = 1;
    }
    public void OnRemoveButton()
    {
        Inventory.InventoryStatic.RemoveItemFromInventoryList(Item, _numberSlot);
        ClearSlot();
    }
    public void UseItem()
    {
        if (Item.IsUsedItem && PotionsControl.CanDrinkAnyPotions)
        {
            _amount--;
            _amountText.text = _amount.ToString();
            Item.Use();
            switch (Item.name)
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
        if (!Item.IsStackable)
        {
            Item.Use();
            OnRemoveButton();
        }
    }
}

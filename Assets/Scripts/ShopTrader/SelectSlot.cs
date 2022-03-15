using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SelectSlot : MonoBehaviour
{
    public event Action UpdatePriceColorsEvent;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerBank _playerBank;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _itemNameText1;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _sellButton;
    [SerializeField] private InventorySlot[] _inventorySlot;
    [SerializeField] private InventorySellSlot[] _inventorySellSlot;
    [SerializeField] private GameObject _sellPanel;
    private Item _item;
    [SerializeField] private int _buySlotPrice;
    private int _slotNumber;
    public void AddBuySlotItem(Item newItem, int price, int slot)
    {
        _item = newItem;
        _icon.sprite = _item.icon;
        _icon.enabled = true;
        _priceText.enabled = true;
        _priceText.text = price.ToString();
        _buySlotPrice = price;
        _slotNumber = slot;
        CheckBuyOrSellItem();
    }

    private void CheckBuyOrSellItem()
    {
        if (_sellPanel.activeInHierarchy)
        {
            _sellButton.interactable = true;
            _buyButton.interactable = false;
        }
        else
        {
            _buyButton.interactable = true;
            _sellButton.interactable = false;
        }
    }
    public void ClearSlot()
    {
        _icon.enabled = false;
        _priceText.enabled = false;
        _item = null;
        _buySlotPrice = 0;
        ButtonsFalse();
    }
    public void BuyItem()
    {
        CustomEvents.FireChangeCoins(-_buySlotPrice);
        Inventory.InventoryStatic.Add(_item);
        ClearSlot();
        UpdatePriceColorsEvent?.Invoke();
        _itemNameText1.text = "Select item";
        ButtonsFalse();
    }

    private void ButtonsFalse()
    {
        _sellButton.interactable = false;
        _buyButton.interactable = false;
    }

    public void SellItem()
    {
        CustomEvents.FireChangeCoins(_buySlotPrice);
        UpdatePriceColorsEvent?.Invoke();
        _itemNameText1.text = "Select item";
        Inventory.InventoryStatic.RemoveItemFromInventoryList(_inventorySellSlot[_slotNumber].Item, _slotNumber);
        _inventorySellSlot[_slotNumber].Item = _inventorySlot[_slotNumber].item;
        _inventorySlot[_slotNumber].OnRemoveButton();
        ClearSlot();
        ButtonsFalse();
        UpdateAllSellSlots();
    }

    private void UpdateAllSellSlots()
    {
        foreach (var slot in _inventorySellSlot)
        {
            slot.UpdateSlot();
        }
    }

    private void OnDisable()
    {
        _icon.enabled = false;
        _priceText.enabled = false;
        _buyButton.interactable = false;
    }
}

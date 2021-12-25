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
    public int BuySlotPrice;

    private int _slotNumber;

    public void AddBuySlotItem(Item newItem, int price, int slot)
    {
        _item = newItem;
        _icon.sprite = _item.icon;
        _icon.enabled = true;
        _priceText.enabled = true;
        _priceText.text = price.ToString();
        BuySlotPrice = price;
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
        BuySlotPrice = 0;
        ButtonsFalse();
    }
    public void BuyItem()
    {
        _playerBank.SpendCoins(BuySlotPrice);
        Inventory.instance.Add(_item);
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
        _playerBank.AddCoins(BuySlotPrice);
        ClearSlot();
        UpdatePriceColorsEvent?.Invoke();
        _itemNameText1.text = "Select item";
        _inventory.items[_slotNumber].RemoveFromInventory();      
        _sellButton.interactable = false;
        _buyButton.interactable = false;
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

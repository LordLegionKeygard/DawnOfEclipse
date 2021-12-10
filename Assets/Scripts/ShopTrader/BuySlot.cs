using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuySlot : MonoBehaviour
{
    public event Action UpdatePriceColorsEvent;
    [SerializeField] private PlayerBank _playerBank;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _itemNameText1;
    [SerializeField] private Button _buyButton;
    private Item _item;
    public int BuySlotPrice;

    public void AddBuySlotItem(Item newItem, int price)
    {
        _item = newItem;
        _icon.sprite = _item.icon;
        _icon.enabled = true;
        _priceText.enabled = true;
        _priceText.text = price.ToString();
        BuySlotPrice = price;
        _buyButton.interactable = true;
    }
    public void ClearSlot()
    {
        _icon.enabled = false;
        _priceText.enabled = false;
        _item = null;
        BuySlotPrice = 0;
        _buyButton.interactable = false;
    }

    public void BuyItem()
    {
        _playerBank.SpendCoins(BuySlotPrice);
        Inventory.instance.Add(_item);
        ClearSlot();
        UpdatePriceColorsEvent?.Invoke();
        _itemNameText1.text = "Select item";
    }

    private void OnDisable()
    {
        _icon.enabled = false;
        _priceText.enabled = false;
        _buyButton.interactable = false;
    }
}

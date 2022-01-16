using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySellSlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private SelectSlot _selectSlot;
    [SerializeField] private InventorySlot _inventorySlot;
    [SerializeField] private PlayerBank _playerBank;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private int _slotNumber;
    public Item Item;

    private void OnEnable()
    {
        UpdateSlot();
    }
    public void UpdateSlot()
    {
        if (_inventorySlot.item != null)
        {
            Item = _inventorySlot.item;
            _priceText.enabled = true;
            _priceText.text = Item.Price.ToString();
            _icon.sprite = _inventorySlot.item.icon;
            _icon.enabled = true;
        }
        else
            ClearSlot();
    }

    public void ClearSlot()
    {
        Item = null;
        _priceText.enabled = false;
        _icon.enabled = false;
    }

    public void SelectItem()
    {
        if (Item != null)
        {
            _selectSlot.AddBuySlotItem(Item, Item.Price, _slotNumber);
            _itemNameText.text = Item.Name[0];
        }
    }
}

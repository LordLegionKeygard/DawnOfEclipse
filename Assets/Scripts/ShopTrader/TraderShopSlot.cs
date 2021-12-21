using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TraderShopSlot : MonoBehaviour
{
    [SerializeField] private PlayerBank _playerBank;
    [SerializeField] private SelectSlot _selectSlot;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private Button _shopSlotButton;
    public int ItemShopPrice;
    public Item Item;
    private void Start()
    {
        _selectSlot.UpdatePriceColorsEvent += UpdatePriceColor;
    }

    public void AddShopItem(Item newItem)
    {
        _shopSlotButton.enabled = true;
        Item = newItem;
        _icon.sprite = Item.icon;
        _icon.enabled = true;
        _priceText.enabled = true;
        _priceText.text = ItemShopPrice.ToString();

        UpdatePriceColor();
    }

    public void SelectItem()
    {
        if (ItemShopPrice <= _playerBank.Coins)
        {
            _selectSlot.AddBuySlotItem(Item, ItemShopPrice, 0);
            _itemNameText.text = Item.Name[0]; // Will need Static Language Number
        }
        else
        {
            _itemNameText.text = "Need more moons";
            _selectSlot.ClearSlot();
        }
    }

    private void UpdatePriceColor()
    {
        if (ItemShopPrice > _playerBank.Coins)
        {
            _priceText.color = Color.black;
        }
        else
            _priceText.color = new Color(1, 0.79f, 0, 1);
    }

    private void OnDisable()
    {
        _icon.enabled = false;
        _priceText.enabled = false;
        _shopSlotButton.enabled = false;
    }
}

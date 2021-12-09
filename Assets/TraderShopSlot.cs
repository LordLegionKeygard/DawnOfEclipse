using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TraderShopSlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _price;
    public int ItemShopPrice;
    private Item _item;

    public void AddShopItem(Item newItem)
    {       
        _item = newItem;
        _icon.sprite = _item.icon;
        _icon.enabled = true;
        _price.enabled = true;
        _price.text = ItemShopPrice.ToString();
    }

    public void BuyItem()
    {
        Inventory.instance.Add(_item);
    }
}

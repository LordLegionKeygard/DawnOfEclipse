using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TraderItems : MonoBehaviour
{
    [SerializeField] private TraderShopSlot[] _shopSlots;
    [SerializeField] private ShopItemPattern[] ItemList = new ShopItemPattern[0];
    [SerializeField] private string _shopLogo;
    [SerializeField] private TextMeshProUGUI _shopTextMeshPro;

    public void AddItemToShop()
    {
        _shopTextMeshPro.text = _shopLogo;

        for (int i = 0; i < ItemList.Length; i++)
        {
            _shopSlots[i].ItemShopPrice = ItemList[i].ItemPrice;
            _shopSlots[i].AddShopItem(ItemList[i].ShopItem);
        }
    }
}

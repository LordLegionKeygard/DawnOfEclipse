using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TraderShopSlot : Slots
{
    [SerializeField] private PlayerBank _playerBank;
    [SerializeField] private SelectSlot _selectSlot;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private Button _shopSlotButton;
    public int ItemShopPrice;

    private void OnEnable()
    {
        _selectSlot.UpdatePriceColorsEvent += UpdatePriceColor;
        CustomEvents.OnSelectItem += SelectSlot;
        CustomEvents.OnUpdateSelectItemTransform += UpdateSelectItemInfoTransform;
    }

    public void AddShopItem(Item newItem)
    {
        _shopSlotButton.enabled = true;
        Item = newItem;
        Icon.sprite = Item.icon;
        Icon.enabled = true;
        _priceText.enabled = true;
        _priceText.text = ItemShopPrice.ToString();

        UpdatePriceColor();
    }

    public void SelectItem()
    {
        if (ItemShopPrice <= _playerBank.Coins)
        {
            _selectSlot.AddBuySlotItem(Item, ItemShopPrice, 0);
            _itemNameText.text = Item.Name[Language.LanguageNumber];
        }
        else
        {
            _itemNameText.text = "Need more moons";
            _selectSlot.ClearSlot();
        }
    }

    private void UpdatePriceColor()
    {
        if (ItemShopPrice > _playerBank.Coins) _priceText.color = Color.black;

        else _priceText.color = new Color(1, 0.79f, 0, 1);
    }

    private void OnDisable()
    {
        _selectSlot.UpdatePriceColorsEvent -= UpdatePriceColor;
        CustomEvents.OnSelectItem -= SelectSlot;
        CustomEvents.OnUpdateSelectItemTransform -= UpdateSelectItemInfoTransform;
        CustomEvents.FireSelectItem(false);
        Icon.enabled = false;
        _priceText.enabled = false;
        _shopSlotButton.enabled = false;
    }


    public void SelectSlot(bool state)
    {

        if (Item.name == "Empty_Item" && state == true) return;
        ResetSetEffectTextColor();
        IsItemSelect = state;

        if (state)
        {
            ItemFrame.color = new Color(0.8980392f, 0.7450981f, 0.1803922f, 1);
            SelectItemInfo.UpdateItemInfoText(Item.Name[Language.LanguageNumber], Item.ItemType[Language.LanguageNumber], Item.ItemInfo[Language.LanguageNumber]);
            if (Item.IsSetEffect)
            {
                SelectItemInfo.UpdateItemSetEffectInfoText
               (Item.AllArmorSetInfo.intArray[Language.LanguageNumber].HelmetInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].ShouldersInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].TorsoInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].ForearmsInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].ElbowsInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].BracersInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].GlovesInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].HipsInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].KneesInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].BootsInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].ThreePiecesInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].FourPiecesInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].FivePiecesInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].SixPiecesInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].SevenPiecesInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].EightPiecesInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].NinePiecesInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].TenPiecesInfo[(int)Item.ArmorSetEnum]);
            }
            else
            {
                SelectItemInfo.ToggleSetEffect(false);
            }
            UpdateSelectItemInfoTransform();
            CustomEvents.FireTooltipToggle(true);
        }

        else
            ItemFrame.color = new Color(0.3301887f, 0.3283905f, 0.1759968f, 0.454902f);
    }

    private void ResetSetEffectTextColor()
    {
        for (int i = 4; i < SelectItemInfo._itemText.Length; i++)
        {
            SelectItemInfo._itemText[i].color = new Color(0.254902f, 0.254902f, 0.2509804f, 1);
        }
    }
}

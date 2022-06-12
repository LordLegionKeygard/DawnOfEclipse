using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySellSlot : Slots
{
    [SerializeField] private SelectSlot _selectSlot;
    [SerializeField] private InventorySlot _inventorySlot;
    [SerializeField] private PlayerBank _playerBank;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private int _slotNumber;

    private void OnEnable()
    {
        UpdateSlot();
        CustomEvents.OnSelectItem += SelectSlot;
        CustomEvents.OnUpdateSelectItemTransform += UpdateSelectItemInfoTransform;
    }
    public void UpdateSlot()
    {
        if (_inventorySlot.Item.name != "Empty_Item")
        {
            Item = _inventorySlot.Item;
            _priceText.enabled = true;
            _priceText.text = Item.Price.ToString();
            Icon.sprite = _inventorySlot.Item.icon;
            Icon.enabled = true;
        }
        else ClearSlot();
    }

    private void ClearSlot()
    {
        _priceText.enabled = false;
        Icon.enabled = false;
    }

    public void SelectItem()
    {
        if (Item != null)
        {
            _selectSlot.AddBuySlotItem(Item, Item.Price, _slotNumber);
            _itemNameText.text = Item.Name[Language.LanguageNumber];
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnSelectItem -= SelectSlot;
        CustomEvents.OnUpdateSelectItemTransform -= UpdateSelectItemInfoTransform;
        CustomEvents.FireSelectItem(false);
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

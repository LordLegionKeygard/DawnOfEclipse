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
    [SerializeField] private Image _icon;
    [SerializeField] private Image _itemFrame;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private GameObject _dropItemButton;
    [SerializeField] private SelectItemInfo _selectItemInfo;
    public bool IsItemSelect = false;
    public Item Item;

    private void OnEnable()
    {
        CustomEvents.OnSelectItem += SelectSlot;
        CustomEvents.OnUpdateSelectItemTransform += UpdateSelectItemInfoTransform;
    }

    private void Update()
    {
        if (!IsCursor) return;

        transform.position = Input.mousePosition;
    }

    public void AddItem(Item newItem, string name)
    {
        Item = newItem;
        _icon.sprite = Item.icon;
        if (newItem.name != "Empty_Item")
            _icon.enabled = true;
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
        _icon.sprite = null;
        _icon.enabled = false;
        _amountText.enabled = false;
        _amount = 1;
    }
    public void OnRemoveItem()
    {
        CustomEvents.FireTooltipToggle(false);
        IsItemSelect = false;
        _dropItemButton.SetActive(false);
        _itemFrame.color = new Color(0.3301887f, 0.3283905f, 0.1759968f, 0.454902f);
        Inventory.InventoryStatic.RemoveItemFromInventoryList(Item, _numberSlot);
        ClearSlot();
    }
    public void UseItem()
    {
        CustomEvents.FireSelectItem(false);
        CustomEvents.FireTooltipToggle(false);
        _dropItemButton.SetActive(false);
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
                OnRemoveItem();
            }
            return;
        }
        if (!Item.IsStackable)
        {
            Item.Use();
            OnRemoveItem();
        }
    }

    public void SelectSlot(bool state)
    {
        if (Item.name == "Empty_Item" && state == true) return;

        _dropItemButton.SetActive(state);
        IsItemSelect = (state);

        if (state)
        {
            _itemFrame.color = new Color(0.8980392f, 0.7450981f, 0.1803922f, 1);
            _selectItemInfo.UpdateItemInfoText(Item.Name[Language.LanguageNumber], Item.ItemType[Language.LanguageNumber], Item.ItemInfo[Language.LanguageNumber]);
            if (Item.IsSetEffect)
            {
                _selectItemInfo.UpdateItemSetEffectInfoText
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
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].FivePiecesInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].SixPiecesInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].SevenPiecesInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].EightPiecesInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].NinePiecesInfo[(int)Item.ArmorSetEnum],
                Item.AllArmorSetInfo.intArray[Language.LanguageNumber].TenPiecesInfo[(int)Item.ArmorSetEnum]);
            }
            else
            {
                _selectItemInfo.ToggleSetEffect(false);
            }
            UpdateSelectItemInfoTransform();
            CustomEvents.FireTooltipToggle(true);
        }

        else
            _itemFrame.color = new Color(0.3301887f, 0.3283905f, 0.1759968f, 0.454902f);
    }

    private void UpdateSelectItemInfoTransform()
    {
        if (!IsItemSelect) return;
        _selectItemInfo.UpdateTransform(new Vector2(transform.position.x, transform.position.y));
    }

    public void DropItem()
    {
        OnRemoveItem();
    }

    private void OnDisable()
    {
        CustomEvents.OnSelectItem -= SelectSlot;
        CustomEvents.OnUpdateSelectItemTransform -= UpdateSelectItemInfoTransform;
        SelectSlot(false);
    }
}

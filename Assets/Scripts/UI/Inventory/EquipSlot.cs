using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : Slots
{
    public Image BackIcon;
    [SerializeField] private EquipmentSlot _equipmentSlot;
    [SerializeField] private ArmorControl _armorControl;
    [SerializeField] private MagicArmorControl _magicArmorControl;
    [SerializeField] private EquipmentManager _equipmentManager;

    public void OnEnable()
    {
        CustomEvents.OnSelectItem += SelectSlot;
        CustomEvents.OnUpdateSelectItemTransform += UpdateSelectItemInfoTransform;
        CustomEvents.OnCheckEquipItemSetNumber += CheckEquipItemSetEffect;
    }

    public void EquipIcon()
    {
        IsItemSelect = false;
        ItemFrame.color = new Color(0.3301887f, 0.3283905f, 0.1759968f, 0.454902f);
        Icon.enabled = true;
        BackIcon.enabled = false;
    }
    public void Unequip()
    {
        ChangeSetEffectTextColor(new Color(0.3882353f, 0.2705882f, 0, 1));
        IsItemSelect = false;
        CustomEvents.FireSelectItem(false);
        CustomEvents.FireTooltipToggle(false, 0);
        SelectSlot(false);
        if (Inventory.InventoryStatic.FullInventory)
        {
            Debug.Log("Can't unequip item, because Inventory is Full");
            return;
        }
        Item = null;
        BackIcon.enabled = true;
        Icon.enabled = false;
        _equipmentManager.Unequip((int)_equipmentSlot);

        switch ((int)_equipmentSlot)
        {
            case 0:
                _armorControl.HeadSlotArmor = DefaultArmor.HeadSlot;
                _equipmentManager.AllHeadElementsToggle(true);
                _equipmentManager.InActiveAllHeadAttachment();
                break;
            case 1:
                _equipmentManager.Equip(_equipmentManager.DefaultEquipment[0]);
                break;
            case 2:
                _equipmentManager.Equip(_equipmentManager.DefaultEquipment[1]);
                break;
            case 4:
                _armorControl.LegsArmor = DefaultArmor.Legs;
                break;
            case 6:
                _armorControl.HandsArmor = DefaultArmor.Hands;
                break;
            case 8:
                _armorControl.ArmUppers = DefaultArmor.ArmUppers;
                break;
            case 10:
                _armorControl.ArmLowers = DefaultArmor.ArmLowers;
                break;
            case 11:
                _magicArmorControl.BackAttachmentMagicArmor = 0;
                break;
            case 13:
                _armorControl.Shoulders = 0;
                break;
            case 15:
                _armorControl.Elbows = 0;
                break;
            case 18:
                _armorControl.Knees = 0;
                break;
            case 19: //AllWeaponSlot
                _equipmentManager.Unequip(19);
                _equipmentManager.Unequip(20);
                _equipmentManager.Unequip(27);
                _equipmentManager.Unequip(28);
                _equipmentManager.Unequip(29);
                _equipmentManager.Unequip(30);
                _equipmentManager.Unequip(32);
                if (_equipmentManager._leftHandSlotListener.enabled == false)
                {
                    _equipmentManager.UnequipTwoHandedWeaponFromShield();
                }
                _equipmentManager.ResetAnimator();
                break;
            case 21: //ExtraSlot
                _equipmentManager.Unequip(21);
                _equipmentManager.Unequip(31);
                _armorControl.ShieldArmorPassive = 0;
                break;
            case 22:
                _magicArmorControl.LeftRingMagicArmor = DefaultArmor.Ring;
                break;
            case 23:
                _magicArmorControl.RightRingMagicArmor = DefaultArmor.Ring;
                break;
            case 24:
                _magicArmorControl.LeftEarringMagicArmor = DefaultArmor.Earring;
                break;
            case 25:
                _magicArmorControl.RightEarringMagicArmor = DefaultArmor.Earring;
                break;
            case 26:
                _magicArmorControl.NecklaceMagicArmor = DefaultArmor.Necklace;
                break;
        }
        _armorControl.UpdateArmor();
        _magicArmorControl.UpdateMagicArmor();
        CustomEvents.FireCheckFullInventory();
    }

    public void SelectSlot(bool state)
    {
        if (Item == null && state == true) return;

        IsItemSelect = state;

        if (state)
        {
            ItemFrame.color = new Color(0.8980392f, 0.7450981f, 0.1803922f, 1);
            SelectItemInfo.UpdateItemInfoText(Item.Name[Language.LanguageNumber], Item.ItemType[Language.LanguageNumber], Item.ItemInfo[Language.LanguageNumber]);
            if (Item.IsSetEffect)
            {
                var p = Item.AllArmorSetInfo.intArray[Language.LanguageNumber];
                SelectItemInfo.UpdateItemSetEffectInfoText
               (p.HelmetInfo[(int)Item.ArmorSetEnum],
                p.ShouldersInfo[(int)Item.ArmorSetEnum],
                p.TorsoInfo[(int)Item.ArmorSetEnum],
                p.ForearmsInfo[(int)Item.ArmorSetEnum],
                p.ElbowsInfo[(int)Item.ArmorSetEnum],
                p.BracersInfo[(int)Item.ArmorSetEnum],
                p.GlovesInfo[(int)Item.ArmorSetEnum],
                p.HipsInfo[(int)Item.ArmorSetEnum],
                p.KneesInfo[(int)Item.ArmorSetEnum],
                p.BootsInfo[(int)Item.ArmorSetEnum],
                p.ThreePiecesInfo[(int)Item.ArmorSetEnum],
                p.FourPiecesInfo[(int)Item.ArmorSetEnum],
                p.FivePiecesInfo[(int)Item.ArmorSetEnum],
                p.SixPiecesInfo[(int)Item.ArmorSetEnum],
                p.SevenPiecesInfo[(int)Item.ArmorSetEnum],
                p.EightPiecesInfo[(int)Item.ArmorSetEnum],
                p.NinePiecesInfo[(int)Item.ArmorSetEnum],
                p.TenPiecesInfo[(int)Item.ArmorSetEnum]);
            }
            else
            {
                SelectItemInfo.ToggleSetEffect(false);
            }
            UpdateSelectItemInfoTransform();
            CustomEvents.FireCheckEquipItemSetNumber((int)Item.ArmorSetEnum);
            CustomEvents.FireTooltipToggle(true, 0);
        }

        else
            ItemFrame.color = new Color(0.3301887f, 0.3283905f, 0.1759968f, 0.454902f);
    }
    private void CheckEquipItemSetEffect(int setEnum)
    {
        ChangeSetEffectTextColor(new Color(0.3882353f, 0.2705882f, 0, 1));
        if (Item == null) return;
        if ((int)Item.ArmorSetEnum == setEnum)
        {
            ChangeSetEffectTextColor(new Color(0.8352941f, 0.8196079f, 0.5294118f, 1));
        }
    }

    private void CheckHowMuchPiecesSetEffect()
    {
        ResetPiecesSetEffectTextColor();
        var setPieces = 0;
        for (int i = 0; i < SelectItemInfo._itemText.Length; i++)
        {
            if (SelectItemInfo._itemText[i].color == new Color(0.8352941f, 0.8196079f, 0.5294118f, 1))
            {
                setPieces++;
            }
        }

        for (int i = 14; i < SelectItemInfo._itemText.Length; i++)
        {
            if (i < 12 + setPieces)
                SelectItemInfo._itemText[i].color = new Color(0.8274511f, 0.5882353f, 0.01568628f, 1);
        }
    }

    private void ResetPiecesSetEffectTextColor()
    {
        for (int i = 14; i < SelectItemInfo._itemText.Length; i++)
        {
            SelectItemInfo._itemText[i].color = new Color(0.254902f, 0.254902f, 0.2509804f, 1);
        }
    }

    private void ChangeSetEffectTextColor(Color color)
    {
        switch (_equipmentSlot)
        {
            case EquipmentSlot.HeadSlot:
                SelectItemInfo._itemText[4].color = color;
                break;
            case EquipmentSlot.Torso:
                SelectItemInfo._itemText[6].color = color;
                break;
            case EquipmentSlot.Hips:
                SelectItemInfo._itemText[11].color = color;
                break;
            case EquipmentSlot.Legs:
                SelectItemInfo._itemText[13].color = color;
                break;
            case EquipmentSlot.Shoulders:
                SelectItemInfo._itemText[5].color = color;
                break;
            case EquipmentSlot.ArmUppers:
                SelectItemInfo._itemText[7].color = color;
                break;
            case EquipmentSlot.ArmLowers:
                SelectItemInfo._itemText[9].color = color;
                break;
            case EquipmentSlot.Elbows:
                SelectItemInfo._itemText[8].color = color;
                break;
            case EquipmentSlot.Hands:
                SelectItemInfo._itemText[10].color = color;
                break;
            case EquipmentSlot.Knees:
                SelectItemInfo._itemText[12].color = color;
                break;
        }
        CheckHowMuchPiecesSetEffect();
    }

    private void OnDisable()
    {
        CustomEvents.OnSelectItem -= SelectSlot;
        CustomEvents.OnCheckEquipItemSetNumber -= CheckEquipItemSetEffect;
        CustomEvents.OnUpdateSelectItemTransform -= UpdateSelectItemInfoTransform;
        SelectSlot(false);
    }
}
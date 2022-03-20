using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    public Image Icon;
    public Image BackIcon;
    [SerializeField] private EquipmentSlot _equipmentSlot;
    [SerializeField] private ArmorControl _armorControl;
    [SerializeField] private MagicArmorControl _magicArmorControl;
    [SerializeField] private EquipmentManager _equipmentManager;

    public void EquipIcon()
    {
        Icon.enabled = true;
        BackIcon.enabled = false;
    }
    public void Unequip()
    {
        if (Inventory.InventoryStatic.FullInventory)
        {
            Debug.Log("Can't unequip item, because Inventory is Full");
            return;
        }
        BackIcon.enabled = true;
        Icon.enabled = false;
        _equipmentManager.Unequip((int)_equipmentSlot);

        switch ((int)_equipmentSlot)
        {
            case 0:
                _armorControl.HeadSlotArmor = 0;
                _equipmentManager.ActiveAllHeadElements(0);
                _equipmentManager.InActiveAllHeadAttachment();
                break;
            case 1:
                _equipmentManager.Equip(_equipmentManager.DefaultEquipment[1]);
                break;
            case 2:
                _equipmentManager.Equip(_equipmentManager.DefaultEquipment[2]);
                break;
            case 3:
                _equipmentManager.Equip(_equipmentManager.DefaultEquipment[6]);
                break;
            case 4:
                _equipmentManager.Equip(_equipmentManager.DefaultEquipment[9]);
                break;
            case 5:
                _equipmentManager.Equip(_equipmentManager.DefaultEquipment[0]);
                break;
            case 6:
                _equipmentManager.Equip(_equipmentManager.DefaultEquipment[3]);
                break;
            case 7:
                _equipmentManager.Equip(_equipmentManager.DefaultEquipment[7]);
                break;
            case 8:
                _equipmentManager.Equip(_equipmentManager.DefaultEquipment[8]);
                break;
            case 9:
                _equipmentManager.Equip(_equipmentManager.DefaultEquipment[4]);
                break;
            case 10:
                _equipmentManager.Equip(_equipmentManager.DefaultEquipment[5]);
                break;
            case 11:
                _armorControl.BackAttachmentArmor = 0;
                break;
            case 12:
                _armorControl.ShoulderRightArmor = 0;
                break;
            case 13:
                _armorControl.ShoulderLeftArmor = 0;
                break;
            case 14:
                _armorControl.ElbowRightArmor = 0;
                break;
            case 15:
                _armorControl.ElbowLeftArmor = 0;
                break;
            case 17:
                _armorControl.KneeRightArmor = 0;
                break;
            case 18:
                _armorControl.KneeLeftArmor = 0;
                break;
            case 19:
                _equipmentManager.Unequip(19);
                _equipmentManager.Unequip(20);
                if (_equipmentManager.ShieldButton.enabled == false)
                {
                    _equipmentManager.UnequipTwoHandedWeaponFromShield();
                }
                _equipmentManager.ResetAnimator();
                break;
            case 21:
                _equipmentManager.Unequip(21);
                _armorControl.ShieldArmorPassive = 0;
                break;
            case 22:
                _magicArmorControl.LeftRingMagicArmor = 0;
                break;
            case 23:
                _magicArmorControl.RightRingMagicArmor = 0;
                break;

        }
        _armorControl.UpdateArmor();
        _magicArmorControl.UpdateMagicArmor();
        CustomEvents.FireCheckFullInventory();
    }
}
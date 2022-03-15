using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    public Image icon;
    public Image backIcon;
    public EquipmentSlot equipmentSlot;
    private ArmorControl armorControl;
    public EquipmentManager equipmentManager;

    private void Start()
    {
        armorControl = FindObjectOfType<ArmorControl>();
    }
    public void Icon()
    {
        icon.enabled = true;
        backIcon.enabled = false;
    }
    public void Unequip()
    {
        if(Inventory.InventoryStatic.FullInventory)
        {
            Debug.Log("Can't unequip item, because Inventory is Full");
            return;
        }
        backIcon.enabled = true;
        icon.enabled = false;
        equipmentManager.Unequip((int)equipmentSlot);

        switch ((int)equipmentSlot)
        {
            case 0:
                armorControl.headSlotArmor = 0;
                equipmentManager.ActiveAllHeadElements(0);
                equipmentManager.InActiveAllHeadAttachment();
                break;
            case 1:
                equipmentManager.Equip(equipmentManager.DefaultEquipment[1]);
                break;
            case 2:
                equipmentManager.Equip(equipmentManager.DefaultEquipment[2]);
                break;
            case 3:
                equipmentManager.Equip(equipmentManager.DefaultEquipment[6]);
                break;
            case 4:
                equipmentManager.Equip(equipmentManager.DefaultEquipment[9]);
                break;
            case 5:
                equipmentManager.Equip(equipmentManager.DefaultEquipment[0]);
                break;
            case 6:
                equipmentManager.Equip(equipmentManager.DefaultEquipment[3]);
                break;
            case 7:
                equipmentManager.Equip(equipmentManager.DefaultEquipment[7]);
                break;
            case 8:
                equipmentManager.Equip(equipmentManager.DefaultEquipment[8]);
                break;
            case 9:
                equipmentManager.Equip(equipmentManager.DefaultEquipment[4]);
                break;
            case 10:
                equipmentManager.Equip(equipmentManager.DefaultEquipment[5]);
                break;
            case 11:
                armorControl.backAttachmentArmor = 0;
                break;
            case 12:
                armorControl.shoulderRightArmor = 0;
                break;
            case 13:
                armorControl.shoulderLeftArmor = 0;
                break;
            case 14:
                armorControl.elbowRightArmor = 0;
                break;
            case 15:
                armorControl.elbowLeftArmor = 0;
                break;
            case 17:
                armorControl.kneeRightArmor = 0;
                break;
            case 18:
                armorControl.kneeLeftArmor = 0;
                break;
            case 19:
                equipmentManager.Unequip(19);
                equipmentManager.Unequip(20);
                if(equipmentManager.ShieldButton.enabled == false)
                {
                    equipmentManager.UnequipTwoHandedWeaponFromShield();
                }                           
                equipmentManager.ResetAnimator();
                break;
            case 21:
                equipmentManager.Unequip(21);
                armorControl.shieldArmorPassive = 0;
                break;
        }
        armorControl.UpdateArmor();
        CustomEvents.FireCheckFullInventory();
    }
}
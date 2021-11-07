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
    public void Equip()
    {
        icon.enabled = true;
        backIcon.enabled = false;
    }
    public void Unequip()
    {
        backIcon.enabled = true;
        icon.enabled = false;
        equipmentManager.Unequip((int)equipmentSlot);

        switch ((int)equipmentSlot)
        {
            case 0:
                armorControl.headSlotArmor = 0;
                break;
            case 1:
                equipmentManager.Equip(equipmentManager.defaultEquipment[0]);
                break;
            case 12:
                armorControl.shoulderRightArmor = 0;
                break;
            case 13:
                armorControl.shoulderLeftArmor = 0;
                break;
        }
        armorControl.UpdateArmor();
    }
}

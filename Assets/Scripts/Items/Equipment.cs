using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* An Item that can be equipped. */

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot; // Slot to store equipment in
    public int armorModifier;       // Increase/decrease in armor
    public SkinnedMeshRenderer mesh;
    public GameObject prefab;
    public int hatNumber;  
    public int noHair; // 0 = withHair, 1 = noHair & noEars, 2 = fullHelmet(noAll)
    public bool canChangehead;

    // When pressed in inventory
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);  // Equip it
        RemoveFromInventory();                  // Remove it from inventory
    }

}

public enum EquipmentSlot 
{ 
HeadSlot = 0, 
Torso = 1, 
Hips = 2, 
LegRight = 3, 
LegLeft = 4, 
HandRight = 5, 
HandLeft = 6, 
ArmUpperRight = 7, 
ArmUpperLeft = 8, 
ArmLowerRight = 9, 
ArmLowerLeft = 10, 
BackAttachment = 11, 
ShoulderRight = 12, 
ShoulderLeft = 13, 
ElbowRight = 14, 
ElbowLeft = 15,
HipsAttachment = 16,
KneeRight = 17,
KneeLeft = 18,
WeaponTwoHand = 19
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public int armorModifier;
    public int MagicArmorModifier;
    public SkinnedMeshRenderer mesh;
    public GameObject prefab;
    public int hatNumber;
    public int noHair; // 0 = withHair, 1 = noHair & noEars, 2 = fullHelmet(noAll)
    public bool canChangehead;
    public bool weapon;
    public bool twoHandedWeapon;
    public bool shield;
    public int shieldBlockArmorModifier;
    public int FirstAttachmentNumber;
    public int SecondAttachmentNumber;
    public override void Use()
    {
        base.Use();
        EquipmentManager.Instance.Equip(this);
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
    WeaponTwoHand = 19,
    WeaponRightHand = 20,
    Shield = 21,
    LeftRing = 22,
    RightRing = 23,
    LeftEarring = 24,
    RightEarring = 25,
    Necklace = 26
}
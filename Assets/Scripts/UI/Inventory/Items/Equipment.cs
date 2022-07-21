using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public int armorModifier;
    public int MagicArmorModifier;
    public SkinnedMeshRenderer[] Meshes;
    public GameObject prefab;
    public bool weapon;
    public bool twoHandedWeapon;
    public bool extraItem;
    public int shieldBlockArmorModifier;

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
    Legs = 4,
    HandRight = 5,
    Hands = 6,
    ArmUpperRight = 7,
    ArmUppers = 8,
    ArmLowerRight = 9,
    ArmLowers = 10,
    BackAttachment = 11,
    ShoulderRight = 12,
    Shoulders = 13,
    ElbowRight = 14,
    Elbows = 15,
    HipsAttachment = 16,
    KneeRight = 17,
    Knees = 18,
    GreatSword = 19,
    StraightSword = 20,
    Shield = 21,
    LeftRing = 22,
    RightRing = 23,
    LeftEarring = 24,
    RightEarring = 25,
    Necklace = 26,
    Hammer = 27,
    DualDaggers = 28,
    Staff = 29,
    Bow = 30,
    QuiverArrow = 31,
    Spear = 32,
    Claws = 33,
}
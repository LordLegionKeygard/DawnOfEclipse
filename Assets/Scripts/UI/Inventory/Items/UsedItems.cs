using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/UsedItem")]
public class UsedItems : Item
{
    public PotionType PotionType;
    public override void Use()
    {
        base.Use();
    }
}

public enum PotionType
{
    Health = 0,
    Speed = 1,
    Mana = 2
}

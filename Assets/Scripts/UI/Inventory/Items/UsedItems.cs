using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/UsedItem")]
public class UsedItems : Item
{
    public float PotionCooldown;
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
    Mana = 2,
    Mead = 3, //медовуха
    Wine = 4, //вино
    Ale = 5, //Эль

}

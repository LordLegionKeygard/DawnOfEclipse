using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddElementalArmorBuff : BaseBuff
{
    [SerializeField] private ElementalAttribute _elementalAttribute;

    public override void Start()
    {
        base.Start();
        CustomEvents.FireElementalArmorBuff((int)_elementalAttribute, Amount);
    }

    public override void EndBuff()
    {
        CustomEvents.FireElementalArmorBuff((int)_elementalAttribute, -Amount);
        Destroy(gameObject);
    }


}

[System.Serializable]
public enum ElementalAttribute
{
    Dark = 0,
    Fire = 1,
    Ice = 2,
    Light = 3,
    Nature = 4,
    Storm = 5,
    Arcane = 6,
    Blood = 7,
}

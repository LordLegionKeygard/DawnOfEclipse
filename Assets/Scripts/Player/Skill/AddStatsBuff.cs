using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStatsBuff : BaseBuff
{
    [SerializeField] private StatAttribute _statAttribute;

    public override void Start()
    {
        base.Start();
        CustomEvents.FireStatBuff((int)_statAttribute, Amount);
    }

    public override void EndBuff()
    {
        CustomEvents.FireStatBuff((int)_statAttribute, -Amount);
        Destroy(gameObject);
    }

    [System.Serializable]
    public enum StatAttribute
    {
        None = -1,
        Strength = 0,
        Dexterity = 1,
        Constitution = 2,
        Endurance = 3,
        Intelligence = 4,
        Wisdom = 5,
        Mind = 6,
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuff : MonoBehaviour
{
    public int Amount;
    public SkillInfo SkillInfo;
    public UsedItems UsedItem;
    public int BuffNumber;

    public virtual void Start()
    {
        CustomEvents.FireCheckIdenticalBuff(BuffNumber);
        CustomEvents.OnCheckIdenticalBuff += DestroyIdenticalBuff;
        if (SkillInfo != null)
        {
            Invoke("EndBuff", SkillInfo.BuffCooldown);
        }
        else
        {
            Invoke("EndBuff", UsedItem.PotionCooldown);
        }
    }

    public virtual void EndBuff()
    {
        Destroy(gameObject);
    }

    protected virtual void DestroyIdenticalBuff(int number)
    {
        if (BuffNumber == number) EndBuff();
    }

    protected virtual void OnDestroy()
    {
        CustomEvents.OnCheckIdenticalBuff -= DestroyIdenticalBuff;
    }
}

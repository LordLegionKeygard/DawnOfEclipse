using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuff : MonoBehaviour
{
    public int Amount;
    public SkillInfo SkillInfo;
    public int BuffNumber;

    public virtual void Start()
    {
        CustomEvents.FireCheckIdenticalBuff(BuffNumber);
        CustomEvents.OnCheckIdenticalBuff += DestroyIdenticalBuff;
        // CustomEvents.FireBuff(_characterStat, Amount);
        Invoke("EndBuff", SkillInfo.BuffCooldown);
    }

    public virtual void EndBuff()
    {
        // CustomEvents.FireBuff(_characterStat, -Amount);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuff : MonoBehaviour
{
    [SerializeField] private int _characterStat;
    [SerializeField] private int _amount;
    [SerializeField] private SkillInfo _skillInfo;

    private void Start()
    {
        CustomEvents.FireBuff(_characterStat, _amount);
        Invoke("EndBuff", _skillInfo.BuffCooldown);
    }

    private void EndBuff()
    {
        CustomEvents.FireBuff(_characterStat, -_amount);
        Destroy(gameObject);
    }
}

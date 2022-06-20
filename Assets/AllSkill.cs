using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSkill : Skills
{
    [SerializeField] private GameObject[] _skillCastPrefab;
    [SerializeField] private GameObject[] _skillPrefab;
    [SerializeField] private Transform[] _castPoint;
    [SerializeField] private Transform[] _skillPoint;
    [SerializeField] private PlayerAnimatorManager _playerAnimatorManager;

    public override void DoSkill()
    {
        Cast();
        CustomEvents.FireAimImageToggle(true);

        switch (SkillInfo[SkillCount].AnimationNumber)
        {
            case 0:
                _playerAnimatorManager.AnimatorProjectileTrigger();
                break;
            case 1:
                _playerAnimatorManager.AnimatorShieldTrigger();
                break;
            case 2:
                _playerAnimatorManager.AnimatorBuffTrigger();
                break;
        }
    }

    private void Cast()
    {
        var cast = Instantiate(_skillCastPrefab[SkillCount], _castPoint[SkillInfo[SkillCount].CastPointNumber].position, Quaternion.identity);
        cast.transform.SetParent(_castPoint[SkillInfo[SkillCount].CastPointNumber].transform);
        Invoke("CastSkill", SkillInfo[SkillCount].TimeToCastSkill);
    }

    private void CastSkill()
    {
        Vector3 aimDir = (StaffTargetAim.MouseWorldPosition - _skillPoint[SkillInfo[SkillCount].SkillPointNumber].position).normalized;
        Debug.Log(aimDir);
        var castSkill = Instantiate(_skillPrefab[SkillCount], _skillPoint[SkillInfo[SkillCount].SkillPointNumber].position, Quaternion.LookRotation(aimDir, Vector3.up));

        if (SkillInfo[SkillCount].IsBuff)
        {
            castSkill.transform.SetParent(_skillPoint[SkillInfo[SkillCount].SkillPointNumber].transform);
        }
    }
}

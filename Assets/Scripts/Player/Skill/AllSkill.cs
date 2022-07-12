using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSkill : Skills
{
    [SerializeField] private Transform[] _castPoint;
    [SerializeField] private Transform[] _skillPoint;
    [SerializeField] private PlayerAnimatorManager _playerAnimatorManager;
    [SerializeField] private BuffIconSpawner _buffIconSpawner;

    public override void DoSkill()
    {
        Cast();
        CustomEvents.FireAimImageToggle(true);
        _playerAnimatorManager.AnimatorSkillInteger(SkillInfo.AnimationNumber);
    }

    private void Cast()
    {
        var castPoint = _castPoint[SkillInfo.CastPointNumber];

        var cast = Instantiate(SkillInfo.SkillCastPrefab, castPoint.position, Quaternion.identity);
        cast.transform.SetParent(castPoint.transform);
        Invoke("CastSkill", SkillInfo.TimeToCastSkill);
    }

    private void CastSkill()
    {
        var skillPoint = _skillPoint[SkillInfo.SkillPointNumber];

        if (SkillInfo.IsBuff)
        {
            var castSkill = Instantiate(SkillInfo.SkillPrefab, skillPoint.position, skillPoint.rotation);
            castSkill.transform.SetParent(skillPoint.transform);

            _buffIconSpawner.SpawnBuffIcon(SkillInfo);
        }
        else
        {
            if (SkillInfo.NotRotate)
            {
                Instantiate(SkillInfo.SkillPrefab, skillPoint.position, skillPoint.rotation);
            }
            else
            {
                Vector3 aimDir = (StaffTargetAim.MouseWorldPosition - skillPoint.position).normalized;
                Instantiate(SkillInfo.SkillPrefab, skillPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
            }
        }
    }
}

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
    [SerializeField] private GameObject _buffIconsPanel;
    [SerializeField] private GameObject _buffIconsPrefab;

    public override void DoSkill()
    {
        Cast();
        CustomEvents.FireAimImageToggle(true);
        _playerAnimatorManager.AnimatorSkillInteger(SkillInfo[SkillCount].AnimationNumber);
    }

    private void Cast()
    {
        var castPoint = _castPoint[SkillInfo[SkillCount].CastPointNumber];

        var cast = Instantiate(_skillCastPrefab[SkillCount], castPoint.position, Quaternion.identity);
        cast.transform.SetParent(castPoint.transform);
        Invoke("CastSkill", SkillInfo[SkillCount].TimeToCastSkill);
    }

    private void CastSkill()
    {
        var skillPoint = _skillPoint[SkillInfo[SkillCount].SkillPointNumber];

        if (SkillInfo[SkillCount].IsBuff)
        {
            var castSkill = Instantiate(_skillPrefab[SkillCount], skillPoint.position, skillPoint.rotation);
            castSkill.transform.SetParent(skillPoint.transform);
            var buffIcon = Instantiate(_buffIconsPrefab, new Vector3(0,0,0), Quaternion.identity);
            buffIcon.transform.SetParent(_buffIconsPanel.transform);
            buffIcon.GetComponent<BuffIcon>().BuffImage.sprite = SkillInfo[SkillCount].SkillIcon;
            buffIcon.GetComponent<BuffIcon>().BuffCooldown = SkillInfo[SkillCount].BuffCooldown;
        }
        else
        {
            Vector3 aimDir = (StaffTargetAim.MouseWorldPosition - skillPoint.position).normalized;
            Instantiate(_skillPrefab[SkillCount], skillPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Skills
{
    [SerializeField] private PlayerAnimatorManager _playerAnimatorManager;
    [SerializeField] private GameObject _meteorPrefab;
    [SerializeField] private Transform _targetTransform;
    private bool _skillActive;

    private void OnEnable()
    {
        CustomEvents.OnUseTargetSkill += UseSKill;
    }

    public override void SkillTurnOn()
    {
        if (ManaCost > ManaControl.CurrentMana || HealthControl.IsDeath) return;
        if (SkillImage.fillAmount == 1)
        {
            DoSkill();
        }
    }

    public override void DoSkill()
    {
        CustomEvents.FireActiveTargetSkill(true);
        _skillActive = true;
    }

    private void UseSKill()
    {
        if (!_skillActive || ManaCost > ManaControl.CurrentMana || HealthControl.IsDeath) return;
        CustomEvents.FireUseMana(ManaCost);
        SkillImage.fillAmount = 0;
        _skillActive = false;
        _playerAnimatorManager.AnimatorSkillTrigger("Skills", 2);
        Invoke("ResetAnimation", 0.5f);

        Instantiate(_meteorPrefab, new Vector3(_targetTransform.transform.position.x, _targetTransform.transform.position.y + 2, _targetTransform.transform.position.z), Quaternion.identity);
        CustomEvents.FireActiveTargetSkill(false);
    }

    private void ResetAnimation()
    {
        _playerAnimatorManager.AnimatorSkillTrigger("Skills", 0);
    }

    private void OnDisable()
    {
        CustomEvents.OnUseTargetSkill -= UseSKill;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSkillButton : Skills
{
    [SerializeField] private PlayerAnimatorManager _playerAnimatorManager;
    [SerializeField] private GameObject _meteorPrefab;
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
            DoSkill(true);
        }
    }

    public override void DoSkill(bool state)
    {
        if (state)
        {
            CustomEvents.FireActiveTargetSkill(true);
            _skillActive = true;
        }
    }

    private void UseSKill()
    {
        if (!_skillActive || ManaCost > ManaControl.CurrentMana || HealthControl.IsDeath) return;
        ManaControl.UseMana(ManaCost);
        SkillToggle = true;
        SkillImage.fillAmount = 0;
        _skillActive = false;
        _playerAnimatorManager.AnimatorSkillTrigger("Skills", 2);
        Invoke("ResetAnimation", 0.5f);

        Instantiate(_meteorPrefab, new Vector3(TargetTransform.transform.position.x, TargetTransform.transform.position.y + 2, TargetTransform.transform.position.z), Quaternion.identity);
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

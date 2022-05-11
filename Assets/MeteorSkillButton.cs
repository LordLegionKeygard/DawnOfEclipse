using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSkillButton : Skills
{
    [SerializeField] private PlayerAnimatorManager _playerAnimatorManager;
    [SerializeField] private GameObject _meteorPrefab;

    public override void DoSkill(bool state)
    {
        if (state)
        {
            _playerAnimatorManager.AnimatorRaceSkillTrigger();

            Instantiate(_meteorPrefab, new Vector3(TargetTransform.transform.position.x, TargetTransform.transform.position.y, TargetTransform.transform.position.z), Quaternion.identity);
        }
    }
}

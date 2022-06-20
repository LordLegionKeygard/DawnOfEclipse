using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : Skills
{
    [SerializeField] private GameObject[] _skillCastPrefab;
    [SerializeField] private GameObject[] _skillPrefab;
    [SerializeField] private Transform _castPoint;
    [SerializeField] private Transform _skillPoint;
    [SerializeField] private float _timeToProjectile;

    [SerializeField] private PlayerAnimatorManager _playerAnimatorManager;

    public override void DoSkill()
    {
        Cast();
        _playerAnimatorManager.AnimatorProjectileTrigger();
        CustomEvents.FireAimImageToggle(true);
    }

    private void Cast()
    {
        var puk = Instantiate(_skillCastPrefab[SkillCount], _castPoint.position, Quaternion.identity);
        puk.transform.SetParent(_castPoint.transform);
        Invoke("Projectile", _timeToProjectile);
    }

    private void Projectile()
    {
        Vector3 aimDir = (StaffTargetAim.MouseWorldPosition - _skillPoint.position).normalized;
        Debug.Log(aimDir);
        Instantiate(_skillPrefab[SkillCount], _skillPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }
}

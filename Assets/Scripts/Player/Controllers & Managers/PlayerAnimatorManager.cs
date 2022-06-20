using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private PlayerInputController _playerInputController;
    private ManaControl _manaControl;
    public readonly int Speed = Animator.StringToHash("speed");
    public readonly int IsInAir = Animator.StringToHash("isInAir");
    public readonly int Attack_R2 = Animator.StringToHash("AttackR2");
    public readonly int Run = Animator.StringToHash("run");
    public readonly int Sneak = Animator.StringToHash("sneak");
    public readonly int Jump = Animator.StringToHash("jump");
    public readonly int Roll = Animator.StringToHash("roll");
    public readonly int PickUp = Animator.StringToHash("pickUp");
    public readonly int Activate = Animator.StringToHash("activate");
    public readonly int Attack_R1 = Animator.StringToHash("AttackR1");
    public readonly int Block_L1 = Animator.StringToHash("Block(L1)");
    public readonly int Block_React_1 = Animator.StringToHash("Block_React_1");
    public readonly int Block_React_2 = Animator.StringToHash("Block_React_2");
    public readonly int Drink = Animator.StringToHash("drink");
    public readonly int StartGame = Animator.StringToHash("start");
    public readonly int HaveMana = Animator.StringToHash("haveMana");
    public readonly int Projectile = Animator.StringToHash("projectile");
    public readonly int Shield = Animator.StringToHash("buff");
    public readonly int Buff = Animator.StringToHash("shield");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInputController = GetComponent<PlayerInputController>();
        _manaControl = GetComponent<ManaControl>();
        _animator.SetTrigger(StartGame);
    }

    private IEnumerator Start()
    {      
        yield return new WaitForSeconds(3f);
        _playerMovement.enabled = true;
        CustomEvents.OnDropStaff += DropStaff;
    }

    public void PlayTargetBoolAnimation(bool state, int animation, float time, int boolNumber)
    {
        StartCoroutine(ExecuteAfterTime(time));
        IEnumerator ExecuteAfterTime(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);

            _animator.SetBool(animation, state);

            switch (boolNumber)
            {
                case 1:
                    _playerInputController.IsAttack = state;
                    break;
                case 2:
                    _playerInputController.IsBlock = state;
                    break;
                case 3:
                    _playerInputController.IsRoll = state;
                    break;
                case 4:
                    _playerInputController.IsFastRun = state;
                    break;
            }
        }
    }

    public void AnimatorPickUpTrigger() => _animator.SetTrigger(PickUp);
    public void AnimatorActivateTrigger() => _animator.SetTrigger(Activate);
    public void AnimatorProjectileTrigger() => _animator.SetTrigger(Projectile);
    public void AnimatorShieldTrigger() => _animator.SetTrigger(Shield);
    public void AnimatorBuffTrigger() => _animator.SetTrigger(Buff);

    public void AnimatorSkillTrigger(string triggerName, int number)
    {
        _animator.SetInteger(triggerName, number);
    }

    public void BlockReact()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                _animator.SetTrigger(Block_React_1);
                break;
            case 1:
                _animator.SetTrigger(Block_React_2);
                break;
        }
    }

    public void EnableDamageCollider()
    {
        CustomEvents.FireEnabledDamageCollider(true);
    }

    public void DisableDamageCollider()
    {
        CustomEvents.FireEnabledDamageCollider(false);
    }

    public void CastSkillR1()
    {
        CustomEvents.FireUseSkillR1(true);
    }

    public void CastSkillR2()
    {
        CustomEvents.FireUseSkillR2(true);
    }

    public void SkillR1()
    {
        CustomEvents.FireUseSkillR1(false);
        CustomEvents.FireUseMana(StaffManaCost.ManaR1);
    }

    public void SkillR2()
    {
        CustomEvents.FireUseSkillR2(false);
        CustomEvents.FireUseMana(StaffManaCost.ManaR2);
    }

    public void AimArrow()
    {
        CustomEvents.FireShootArrow(false);
    }

    public void ShootArrow()
    {
        CustomEvents.FireShootArrow(true);
    }

    public void Aim()
    {
        CustomEvents.FireAim(true);
        CustomEvents.FireCanRotate(false);
    }

    public void TakeArrow()
    {
        CustomEvents.FireTakeArrow();
        CustomEvents.FireCanRotate(false);
    }
    public void NotAim()
    {
        CustomEvents.FireAim(false);
        CustomEvents.FireCanRotate(true);
    }

    public void CheckManaCostR1()
    {
        _animator.SetBool(HaveMana, _manaControl.CurrentMana >= StaffManaCost.ManaR1);
    }
    public void CheckManaCostR2()
    {
        _animator.SetBool(HaveMana, _manaControl.CurrentMana >= StaffManaCost.ManaR2);
    }

    public void DropStaff()
    {
        _animator.SetBool(HaveMana, true);
    }

    private void OnDestroy()
    {
        CustomEvents.OnDropStaff -= DropStaff;
    }
}

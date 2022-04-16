using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private PlayerInputController _playerInputController;
    public static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int IsInAir = Animator.StringToHash("isInAir");
    private static readonly int Attack_R2_Trigger = Animator.StringToHash("AttackR2");
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Sneak = Animator.StringToHash("sneak");
    private static readonly int Jump = Animator.StringToHash("jump");
    private static readonly int Roll = Animator.StringToHash("roll");
    private static readonly int Attack_R1_Trigger = Animator.StringToHash("AttackR1");
    private static readonly int Block_L1_Trigger = Animator.StringToHash("Block(L1)");
    private static readonly int Block_React_1 = Animator.StringToHash("Block_React_1");
    private static readonly int Block_React_2 = Animator.StringToHash("Block_React_2");
    private static readonly int Drink = Animator.StringToHash("drink");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInputController = GetComponent<PlayerInputController>();
    }

    public void Sneaking()
    {
        _animator.SetBool(Sneak, true);
    }

    public void NotSneaking()
    {
        _animator.SetBool(Sneak, false);
    }

    public void EnableJump()
    {
        _animator.SetBool(Jump, true);
    }

    public void DisableJump()
    {
        _animator.SetBool(Jump, false);
    }

    public void EnableFastRun() => _playerInputController.IsFastRun = true;

    public void DisableFastRun() => _playerInputController.IsFastRun = false;

    public void AttackR2()
    {
        _playerInputController.IsAttack = true;
        _animator.SetBool(Attack_R2_Trigger, true);
    }

    public void AttackR1()
    {
        _playerInputController.IsAttack = true;
        _animator.SetBool(Attack_R1_Trigger, true);
    }
    public void AttackR1FastRun()
    {
        _playerInputController.IsAttack = true;
        _animator.SetBool(Attack_R1_Trigger, true);
    }

    public void BlockL1()
    {
        _playerInputController.IsBlock = true;
        _animator.SetBool(Block_L1_Trigger, true);
    }

    public void BlockReact()
    {
        var rnd = Random.Range(0, 2);
        _playerInputController.IsBlock = true;
        switch (rnd)
        {
            case 0:
                _animator.SetTrigger(Block_React_1);
                break;
            case 1:
                _animator.SetTrigger(Block_React_2);
                break;
        }
    }

    public void DisableBlockL1()
    {
        _playerInputController.IsAttack = false;
        _playerInputController.IsBlock = false;
        _animator.SetBool(Block_L1_Trigger, false);
    }
    public void DisableAttackR2()
    {
        _playerInputController.IsAttack = false;
        _animator.SetBool(Attack_R2_Trigger, false);
    }

    public void DisableAttackR1()
    {
        _playerInputController.IsAttack = false;
        _animator.SetBool(Attack_R1_Trigger, false);
    }

    public void EnableRoll()
    {
        _playerInputController.IsRoll = true;
        _animator.SetBool(Roll, true);
    }

    public void DisableRoll()
    {
        _animator.SetBool(Roll, false);
        _playerInputController.IsRoll = false;
    }
    public void InAir()
    {
        _animator.SetBool(IsInAir, true);
    }
    public void OnGround()
    {
        _animator.SetBool(IsInAir, false);
    }

    public void Running()
    {
        _animator.SetBool(Run, true);
    }

    public void NotRunning()
    {
        _animator.SetBool(Run, false);
    }

    public void EnableDamageCollider()
    {
        CustomEvents.FireEnabledDamageCollider(true);
    }

    public void DisableDamageCollider()
    {
        CustomEvents.FireEnabledDamageCollider(false);
    }
}

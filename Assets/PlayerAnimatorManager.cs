using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    private Animator anim;

    public float velocityMove;
    private PlayerController playerController;
    private static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int IsInAir = Animator.StringToHash("isInAir");
    private static readonly int Attack_R2_Trigger = Animator.StringToHash("AttackR2");
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Sneak = Animator.StringToHash("sneak");
    private static readonly int Jump_Trigger = Animator.StringToHash("jump");
    private static readonly int Roll = Animator.StringToHash("roll");
    private static readonly int Attack_R1_Trigger = Animator.StringToHash("AttackR1");
    private static readonly int Block_L1_Trigger = Animator.StringToHash("Block(L1)");
    private static readonly int Drink = Animator.StringToHash("drink");

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    public void Sneaking()
    {
        anim.SetBool(Sneak, true);
    }

    public void NotSneaking()
    {
        anim.SetBool(Sneak, false);
    }

    public void Speeding()
    {
        anim.SetFloat(Speed, velocityMove);
    }

    public void JumpTrigger()
    {
        anim.SetTrigger(Jump_Trigger);
    }

    public void EnableFastRun() => playerController.fastRun = true;

    public void DisableFastRun() => playerController.fastRun = false;

    public void AttackR2()
    {
        playerController.walk = false;
        playerController.attack = true;
        anim.SetBool(Attack_R2_Trigger, true);
    }

    public void AttackR1()
    {
        playerController.walk = false;
        playerController.attack = true;
        anim.SetBool(Attack_R1_Trigger, true);
    }
    public void AttackR1FastRun()
    {
        playerController.walk = false;
        playerController.attack = true;
        anim.SetBool(Attack_R1_Trigger, true);
    }

    public void BlockL1()
    {
        playerController.walk = false;
        playerController.block = true;
        anim.SetBool(Block_L1_Trigger, true);
    }

    public void DisableBlockL1()
    {
        playerController.walk = true;
        playerController.attack = false;
        playerController.block = false;
        anim.SetBool(Block_L1_Trigger, false);
    }
    public void DisableAttackR2()
    {
        playerController.walk = true;
        playerController.attack = false;
        anim.SetBool(Attack_R2_Trigger, false);
    }

    public void DisableAttackR1()
    {
        playerController.attack = false;
        anim.SetBool(Attack_R1_Trigger, false);
    }

    public void EnableRoll()
    {
        playerController.roll = true;
        anim.SetBool(Roll, true);
    }

    public void DisableRoll()
    {
        anim.SetBool(Roll, false);
        playerController.roll = false;
    }

    public void Drinking()
    {
        anim.SetTrigger(Drink);
    }

    public void InAir()
    {
        anim.SetBool(IsInAir, true);
    }
    public void OnGround()
    {
        anim.SetBool(IsInAir, false);
    }

    public void Running()
    {
        anim.SetBool(Run, true);
    }

    public void NotRunning()
    {
        anim.SetBool(Run, false);
    }
}

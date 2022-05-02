using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private PlayerInputController _playerInputController;
    public readonly int Speed = Animator.StringToHash("speed");
    public readonly int IsInAir = Animator.StringToHash("isInAir");
    public readonly int Attack_R2 = Animator.StringToHash("AttackR2");
    public readonly int Run = Animator.StringToHash("run");
    public readonly int Sneak = Animator.StringToHash("sneak");
    public readonly int Jump = Animator.StringToHash("jump");
    public readonly int Roll = Animator.StringToHash("roll");
    public readonly int PickUp = Animator.StringToHash("pickUp");
    public readonly int Attack_R1 = Animator.StringToHash("AttackR1");
    public readonly int Block_L1 = Animator.StringToHash("Block(L1)");
    public readonly int Block_React_1 = Animator.StringToHash("Block_React_1");
    public readonly int Block_React_2 = Animator.StringToHash("Block_React_2");
    public readonly int Drink = Animator.StringToHash("drink");
    public readonly int RaceSkill = Animator.StringToHash("raceSkill");
    public readonly int StartGame = Animator.StringToHash("start");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInputController = GetComponent<PlayerInputController>();
        _animator.SetTrigger(StartGame);
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

    public void AnimatorRaceSkillTrigger() => _animator.SetTrigger(RaceSkill);

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

    public void EnableDamageCollider()
    {
        CustomEvents.FireEnabledDamageCollider(true);
    }

    public void DisableDamageCollider()
    {
        CustomEvents.FireEnabledDamageCollider(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkillStateMachine : StateMachineBehaviour
{
    [SerializeField] private bool _canUseSkill;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        CustomEvents.FireCanUseSkill(_canUseSkill);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkStateMachine : StateMachineBehaviour
{
    [SerializeField] private bool _canWalk;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        CustomEvents.FireCanWalk(_canWalk);
    }
}

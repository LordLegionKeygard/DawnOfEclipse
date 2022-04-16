using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkStateMachine : StateMachineBehaviour
{
    [SerializeField] private bool _canWalk;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (_canWalk)
            CustomEvents.FireCanWalk(true);
        else
            CustomEvents.FireCanWalk(false);
    }
}

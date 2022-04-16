using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStateMachine : StateMachineBehaviour
{
    [SerializeField] private bool _isBlock;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (_isBlock)
            CustomEvents.FireBlock(true);
        else
            CustomEvents.FireBlock(false);
    }
}

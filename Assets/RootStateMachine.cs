using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootStateMachine : StateMachineBehaviour
{
    [SerializeField] private bool _canRoot;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (_canRoot)
            CustomEvents.FireCanRoot(true);
        else
            CustomEvents.FireCanRoot(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimStateMachine : StateMachineBehaviour
{
    [SerializeField] private bool _canAim;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        CustomEvents.FireAim(_canAim);
        CustomEvents.FireCanRotate(!_canAim);
    }
}

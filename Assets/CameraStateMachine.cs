using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateMachine : StateMachineBehaviour
{
    [SerializeField] private bool _cameraCanMove;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        CustomEvents.FireCameraCanMove(_cameraCanMove);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        
    }
}

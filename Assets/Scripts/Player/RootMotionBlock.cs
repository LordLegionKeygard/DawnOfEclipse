using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionBlock : MonoBehaviour
{
    Animator anim;
    AnimatorStateInfo _stateInfo;

    public bool rootMotion = false;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnAnimatorMove()
    {
        if(rootMotion == true)
        {
            anim.ApplyBuiltinRootMotion();
        }       
    }

    public void CanRoot()
    {
        rootMotion = true;
    }

    public void StopRoot()
    {
        rootMotion = false;
    }
}

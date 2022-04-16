using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionBlock : MonoBehaviour
{
    private Animator _animator;
    private bool _rootMotion;

    private void OnEnable()
    {
        CustomEvents.OnCanRoot += RootToggle;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnAnimatorMove()
    {
        if (_rootMotion == true) { _animator.ApplyBuiltinRootMotion(); }
    }

    private void RootToggle(bool state)
    {
        _rootMotion = state;
    }

    private void OnDisable()
    {
        CustomEvents.OnCanRoot -= RootToggle;
    }
}

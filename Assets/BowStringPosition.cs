using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowStringPosition : MonoBehaviour
{
    [SerializeField] private Transform _aimHand;
    [SerializeField] private Transform _middlePoint;
    [SerializeField] private Transform _defaultMiddlePoint;
    public bool _aim;

    private void OnEnable()
    {
        CustomEvents.OnShootArrow += AimToggle;
    }
    private void Start()
    {
        _aimHand = GameObject.FindGameObjectWithTag("AimHand").transform;
    }

    private void AimToggle(bool state)
    {
        _aim = !state;
    }

    private void Update()
    {
        if (_aim)
        {
            _middlePoint.position = _aimHand.position;
        }
        else
        {
            _middlePoint.position = _defaultMiddlePoint.position;
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnShootArrow -= AimToggle;
    }
}

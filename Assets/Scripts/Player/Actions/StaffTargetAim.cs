using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffTargetAim : MonoBehaviour
{
    [SerializeField] private Transform _debugTransform;
    public static Vector3 MouseWorldPosition;
    public bool _isAim;
    private Transform _cameraTransform;

    private void OnEnable()
    {
        CustomEvents.OnAim += AimToggle;
    }

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Ray ray = new Ray(_cameraTransform.position + Vector3.up * 2, _cameraTransform.forward);

        _debugTransform.position = ray.GetPoint(20);
        MouseWorldPosition = _debugTransform.position;
        
        if (_isAim)
        {
            Vector3 worldAimTarget = MouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
    }


    private void AimToggle(bool state)
    {
        _isAim = state;
    }

    private void OnDisable()
    {
        CustomEvents.OnAim -= AimToggle;
    }
}
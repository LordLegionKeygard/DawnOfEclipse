using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private Transform _arrowPoint;
    [SerializeField] private GameObject _arrowPrefab;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        CustomEvents.OnShootArrow += ShootArrow;
        CustomEvents.FireAimImageToggle(true);
    }

    private void ShootArrow(bool shoot)
    {
        if (!shoot)
        {
            // _animator.SetTrigger("aim");
        }
        if (shoot)
        {
            Debug.Log("Shoot");
            // Vector3 aimDir = (StaffTargetAim.MouseWorldPosition - _arrowPoint.position).normalized;
            // Instantiate(_arrowPrefab, _arrowPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnShootArrow -= ShootArrow;
        CustomEvents.FireAimImageToggle(false);
    }
}

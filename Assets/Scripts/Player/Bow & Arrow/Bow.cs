using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private Transform _arrowPoint;
    public GameObject ArrowPrefab;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        CustomEvents.OnShootArrow += ShootArrow;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3f);
        CustomEvents.FireAimImageToggle(true);
    }

    private void ShootArrow(bool shoot)
    {
        if (ArrowPrefab == null) return;
        if (shoot)
        {
            Vector3 aimDir = (StaffTargetAim.MouseWorldPosition - _arrowPoint.position).normalized;
            Instantiate(ArrowPrefab, _arrowPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnShootArrow -= ShootArrow;
        CustomEvents.FireAimImageToggle(false);
    }
}

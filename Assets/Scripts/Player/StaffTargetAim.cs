using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffTargetAim : MonoBehaviour
{
    [SerializeField] private Transform _debugTransform;
    [SerializeField] private Transform _prefabBulletProjectile;
    [SerializeField] private Transform _spawnBulletPosition;
    [SerializeField] private LayerMask _aimColliderLayerMask = new LayerMask();
    public static Vector3 MouseWorldPosition;

    public bool _isAim;

    private void OnEnable()
    {
        CustomEvents.OnAim += AimToggle;
    }

    private void Update()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f + 150);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, _aimColliderLayerMask))
        {
            _debugTransform.position = Vector3.Lerp(_debugTransform.position, raycastHit.point, 10f * Time.deltaTime);
            MouseWorldPosition = raycastHit.point;
        }

        if (_isAim)
        {
            Vector3 worldAimTarget = MouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }

        // else

        // {
        //     _debugTransform.position = ray.GetPoint(20);
        //     MouseWorldPoisition = _debugTransform.transform.position;
        // }
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
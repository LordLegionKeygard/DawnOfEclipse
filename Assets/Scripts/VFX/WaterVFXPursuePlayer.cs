using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterVFXPursuePlayer : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private bool _water;

    private void OnEnable()
    {
        CustomEvents.OnPlayerInWaterVFX += WaterToggle;
    }

    private void WaterToggle(bool state)
    {
        _water = state;
    }

    private void LateUpdate()
    {
        
        // if(!_water) return;
        transform.position = new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z);
    }

    private void OnDisable()
    {
        CustomEvents.OnPlayerInWaterVFX -= WaterToggle;
    }
}

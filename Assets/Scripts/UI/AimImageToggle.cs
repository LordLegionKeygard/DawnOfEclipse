using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimImageToggle : MonoBehaviour
{
    [SerializeField] private GameObject _aimImage;

    private void OnEnable()
    {
        CustomEvents.OnAimImageToggle += AimToggle;
    }

    private void AimToggle(bool state)
    {
        _aimImage.SetActive(state);
    }

    private void OnDisable()
    {
        CustomEvents.OnAimImageToggle -= AimToggle;
    }
}

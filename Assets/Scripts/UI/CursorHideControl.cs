using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CursorHideControl : MonoBehaviour
{
    [SerializeField] private CinemachineInputProvider _cinemachineInputProvider;

    public int _activeUIwindow;
    private void OnEnable()
    {
        CustomEvents.OnHideCursor += Hide;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Hide(bool state)
    {
        if (state) _activeUIwindow++;
        else _activeUIwindow--;

        if (_activeUIwindow > 0)
        {
            Cursor.visible = true;
            _cinemachineInputProvider.enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
           Cursor.visible = false;
            _cinemachineInputProvider.enabled = true;
            Cursor.lockState = CursorLockMode.Locked; 
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnHideCursor -= Hide;
    }
}

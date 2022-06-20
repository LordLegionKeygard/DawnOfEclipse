using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackgroundPhotoColors : MonoBehaviour
{
    [SerializeField] private int _number;
    [SerializeField] private Color[] _colors;
    [SerializeField] private Camera _camera;

    private void Start()
    {
        _camera.backgroundColor = _colors[_number];
    }
}

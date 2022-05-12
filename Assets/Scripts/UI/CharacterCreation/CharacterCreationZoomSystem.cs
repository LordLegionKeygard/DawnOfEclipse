using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreationZoomSystem : MonoBehaviour
{
    [SerializeField] private Vector3[] _markers;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject[] _plus;
    private bool _zoomIn;
    private float _zoomTime = 2f;


    private void Update()
    {
        if (_zoomIn)
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _markers[1], Time.deltaTime * _zoomTime);
        else
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _markers[0], Time.deltaTime * _zoomTime);
    }

    public void CameraBack()
    {
        _zoomIn = true;
        _markers[1] = _markers[2];
        _zoomTime = 0.5f;
    }

    public void ZoomToggle()
    {
        if (_zoomIn)
        {
            _zoomIn = false;
            PlusToggle(true);
        }
        else
        {
            _zoomIn = true;
            PlusToggle(false);
        }
    }

    private void PlusToggle(bool state)
    {
        foreach (var item in _plus)
        {
            item.SetActive(state);
        }
    }
}

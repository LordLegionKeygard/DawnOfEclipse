using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreationZoomSystem : MonoBehaviour
{
    [SerializeField] private Vector3[] _markers;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _plus;
    private bool _zoomIn;

    private void Update()
    {
        if (_zoomIn)
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _markers[1], Time.deltaTime * 2f);
        else
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _markers[0], Time.deltaTime * 2f);
    }

    public void ZoomToggle()
    {
        if (_zoomIn)
        {
            _zoomIn = false;
            _plus.SetActive(true);
        }
        else
        {
            _zoomIn = true;
            _plus.SetActive(false);
        }
    }
}

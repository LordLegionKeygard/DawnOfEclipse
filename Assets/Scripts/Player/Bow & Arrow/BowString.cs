using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowString : MonoBehaviour
{
    [SerializeField] private LineRenderer _lr;
    [SerializeField] private Transform[] _points;

    private void Start()
    {
        SetUpLine(_points);
    }

    private void SetUpLine(Transform[] points)
    {
        _lr.positionCount = points.Length;
        this._points = points;
    }

    private void LateUpdate()
    {
        _lr.SetPosition(0, _points[0].position);
        _lr.SetPosition(1, _points[1].position);
    }
}

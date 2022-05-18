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

    private void Update()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            _lr.SetPosition(i, _points[i].position);
        }
    }
}

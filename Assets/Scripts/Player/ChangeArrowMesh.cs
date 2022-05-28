using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeArrowMesh : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private Mesh[] _arrowsMesh;

    private void Start()
    {
        CustomEvents.OnUseArrow += ChangeArrow;
    }

    private void ChangeArrow(int arrowNumber)
    {
        _meshFilter.mesh = _arrowsMesh[arrowNumber];
    }

    private void OnDestroy()
    {
        CustomEvents.OnUseArrow -= ChangeArrow;
    }
}

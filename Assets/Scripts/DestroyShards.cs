using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShards : MonoBehaviour
{
    [SerializeField] private ObjectDestroy _objectDestroy;

    public void ShardsDestroy()
    {
        _objectDestroy.Destroy();
    }
}

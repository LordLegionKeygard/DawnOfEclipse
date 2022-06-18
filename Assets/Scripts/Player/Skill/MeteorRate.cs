using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MeteorRate : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private VisualEffect _visualEffect;
    private void Start()
    {
        Invoke("ColliderActive", 1f);
        Invoke("ChangeRate", 10f);
    }

    private void ColliderActive()
    {
        _collider.enabled = true;
    }

    private void ChangeRate()
    {
        _collider.enabled = false;
        _visualEffect.SetFloat("Rate", 0f);
    }
}

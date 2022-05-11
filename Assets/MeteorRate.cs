using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MeteorRate : MonoBehaviour
{
    [SerializeField] private VisualEffect _visualEffect;
    private void Start()
    {
        Invoke("ChangeRate", 10f);
    }

    private void ChangeRate()
    {
        _visualEffect.SetFloat("Rate", 0f);
    }
}

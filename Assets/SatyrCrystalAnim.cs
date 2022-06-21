using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SatyrCrystalAnim : MonoBehaviour
{
    [SerializeField] private float _smooth;

    private void Update()
    {
        gameObject.transform.RotateAround(gameObject.transform.position, Vector3.up, _smooth * Time.deltaTime);
    }
}

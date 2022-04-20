using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistDamageColliderController : MonoBehaviour
{
    [SerializeField] private DamageCollider[] _fistDamageCollider;

    public void FistDamageColliderToggle(bool state)
    {
        _fistDamageCollider[0].enabled = state;
        _fistDamageCollider[1].enabled = state;
    }
}

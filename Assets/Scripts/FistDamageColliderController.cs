using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistDamageColliderController : MonoBehaviour
{
    [SerializeField] private DamageCollider[] _fistDamageCollider;
    [SerializeField] private PoisonDamageCollider[] _posionDamageCollider;

    public void FistDamageColliderToggle(bool state)
    {
        _fistDamageCollider[0].CanDamage = state;
        _fistDamageCollider[1].CanDamage = state;
        _posionDamageCollider[0].CanDamage = state;
        _posionDamageCollider[1].CanDamage = state;
    }
}

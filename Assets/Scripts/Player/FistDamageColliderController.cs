using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistDamageColliderController : MonoBehaviour
{
    [SerializeField] private PhysDamage[] _fistDamageCollider;
    [SerializeField] private PoisonDamageCollider[] _posionDamageCollider;
    [SerializeField] private MagicDamage _magicDamage;

    public void FistDamageColliderToggle(bool state)
    {
        _magicDamage.CanDamage(state);
        _fistDamageCollider[0].CanDamage(state);
        _fistDamageCollider[1].CanDamage(state);
        _posionDamageCollider[0].CanDamage = state;
        _posionDamageCollider[1].CanDamage = state;
    }
}

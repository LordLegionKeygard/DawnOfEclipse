using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFistDamage : MonoBehaviour
{
    [SerializeField] private DamageCollider _damageCollider;

    private void Awake()
    {
        switch (CharacterInformation.Class)
        {
            case 0:
                _damageCollider.BaseWeaponDamage = 4;
                break;
            case 1:
                _damageCollider.BaseWeaponDamage = 3;
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFistDamage : MonoBehaviour
{
    [SerializeField] private PhysDamage _physDamage;

    private void Awake()
    {
        switch (CharacterInformation.Class)
        {
            case 0:
                _physDamage.BaseWeaponDamage = 4;
                break;
            case 1:
                _physDamage.BaseWeaponDamage = 3;
                break;
        }
    }
}

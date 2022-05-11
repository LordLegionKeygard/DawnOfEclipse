using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDamagePerSecond : MonoBehaviour
{
    [SerializeField] private float _magicDamage;



    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent(out EnemyStats enemyStats))
        {
            // if (rnd < _physCritChance)
            // {
                enemyStats.CalculateDamage(_magicDamage, DamageType.MageDamage);
                Debug.Log("Crit");
            // }
            // else
            // {
            //     enemyStats.CalculateDamage(_weaponDamage, DamageType.PhysDamage);
            // }
        }
    }
}

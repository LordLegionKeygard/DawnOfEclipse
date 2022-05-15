using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDamagePerSecond : MonoBehaviour
{
    private void OnTriggerStay(Collider collision)
    {
        var rnd = Random.Range(1, 100);
        if (collision.TryGetComponent(out EnemyStats enemyStats))
        {
            if (rnd < MagicDamage.MagCritChance)
            {
                enemyStats.CalculateDamage(MagicDamage.WeaponMagicDamage * 0.1f, DamageType.PhysDamage);
                Debug.Log("MagicCrit");
            }
            else
            {
                enemyStats.CalculateDamage(MagicDamage.WeaponMagicDamage * 0.05f, DamageType.PhysDamage);
            }
        }
    }
}

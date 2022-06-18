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
            if (rnd < CurrentDamage.CurrentDamageS.CurrentMageCritChance)
            {
                enemyStats.CalculateDamage(CurrentDamage.CurrentDamageS.CurrentWeaponMagicDamage * 0.1f, DamageType.MageDamage, true);
            }
            else
            {
                enemyStats.CalculateDamage(CurrentDamage.CurrentDamageS.CurrentWeaponMagicDamage * 0.05f, DamageType.MageDamage, false);
            }
        }
    }
}

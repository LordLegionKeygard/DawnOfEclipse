using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    private Collider damageCollider;

    public int currentWeaponDamage;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        CustomEvents.OnEnabledDamageCollider += Damage;
    }

    public void Damage(bool isDamage)
    {
        if (isDamage)
            damageCollider.enabled = true;
        else
            damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out EnemyStats enemyStats))
        {
            enemyStats.TakeDamage(currentWeaponDamage);
            Damage(false);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnEnabledDamageCollider -= Damage;
    }
}

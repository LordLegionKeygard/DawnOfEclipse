using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] private int _weaponDamage;
    private Collider _damageCollider;
    public bool CanDamage = true;

    private void OnEnable()
    {
        _damageCollider = GetComponent<Collider>();
        CustomEvents.OnEnabledDamageCollider += Damage;
    }

    public void Damage(bool state)
    {
        _damageCollider.enabled = state;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent(out EnemyStats enemyStats) && CanDamage)
        {
            enemyStats.TakeDamage(_weaponDamage);
            Damage(false);
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnEnabledDamageCollider -= Damage;
    }
}

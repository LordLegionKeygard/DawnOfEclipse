using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    private Collider _damageCollider;

    [SerializeField] private int _weaponDamage;

    private void Awake()
    {
        _damageCollider = GetComponent<Collider>();
        CustomEvents.OnEnabledDamageCollider += Damage;
    }

    public void Damage(bool state)
    {
        _damageCollider.enabled = state;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out EnemyStats enemyStats))
        {
            enemyStats.TakeDamage(_weaponDamage);
            Damage(false);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnEnabledDamageCollider -= Damage;
    }
}

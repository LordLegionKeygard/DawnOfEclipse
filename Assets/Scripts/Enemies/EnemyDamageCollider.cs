using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollider : MonoBehaviour
{
    [SerializeField] private float _weaponDamage;
    private BoxCollider _col;
    private void Awake()
    {
        _col = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out HealthControl healthControl))
        {
            if (healthControl != null)
            {
                healthControl.TakeDamage(_weaponDamage);
                _col.enabled = false;
            }
        }
    }
}

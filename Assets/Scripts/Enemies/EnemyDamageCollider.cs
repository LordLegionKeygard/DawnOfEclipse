using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollider : MonoBehaviour
{
    [SerializeField] private EnemyLevel _enemyLevel;
    public float WeaponDamage;
    private BoxCollider _col;
    private void Awake()
    {
        _col = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        WeaponDamage = _enemyLevel.EnemyInformation.physAttack[_enemyLevel.Level];
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out HealthControl healthControl))
        {
            if (healthControl != null)
            {
                healthControl.TakeDamage(WeaponDamage);
                _col.enabled = false;
            }
        }
    }
}

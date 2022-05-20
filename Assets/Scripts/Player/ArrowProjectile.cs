using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _bulletSpeed;
    private Collider _collider;
    private Rigidbody _rb;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        _rb.velocity = transform.forward * _bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        var rnd = Random.Range(1, 100);
        if (other.TryGetComponent(out EnemyStats enemyStats))
        {
            transform.SetParent(enemyStats.EnemySpine.transform);
            if (rnd < CurrentDamage.CurrentDamageS.CurrentPhysCritChance)
            {
                enemyStats.CalculateDamage(CurrentDamage.CurrentDamageS.CurrentWeaponPhysDamage * 2, DamageType.PhysDamage);
                Debug.Log("MagicCrit");
            }
            else
            {
                enemyStats.CalculateDamage(CurrentDamage.CurrentDamageS.CurrentWeaponPhysDamage, DamageType.PhysDamage);
            }
        }
        if (other.TryGetComponent(out DestroyShards _destroyShards))
        {
            _destroyShards.ShardsDestroy();
            transform.SetParent(other.transform);
        }
        else
        {
            transform.SetParent(other.transform);
        }
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.isKinematic = true;

        _collider.enabled = false;
        Destroy(gameObject, _lifeTime);
    }
}

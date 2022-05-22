using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] private float _arrowExtraDamage;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _bulletSpeed;
    private Collider _collider;
    private Rigidbody _rb;
    private GameObject _otherObject;
    private bool _arrowInEnemy;
    

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        _rb.velocity = transform.forward * _bulletSpeed;
        DestroyArrow();
    }

    private void Update()
    {
        if (!_arrowInEnemy) return;
        transform.position = new Vector3(_otherObject.transform.position.x,transform.position.y,_otherObject.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {       
        var rnd = Random.Range(1, 100);
        if (other.TryGetComponent(out EnemyTakeDamage enemyTakeDamage))
        {
            _arrowInEnemy = true;
            _otherObject = enemyTakeDamage.EnemyStats.EnemySpine.gameObject;
            transform.SetParent(_otherObject.transform);
            if (rnd < CurrentDamage.CurrentDamageS.CurrentPhysCritChance)
            {
                enemyTakeDamage.EnemyStats.CalculateDamage(CurrentDamage.CurrentDamageS.CurrentWeaponPhysDamage * 2 + _arrowExtraDamage, DamageType.PhysDamage);
                Debug.Log("Crit");
            }
            else
            {
                enemyTakeDamage.EnemyStats.CalculateDamage(CurrentDamage.CurrentDamageS.CurrentWeaponPhysDamage + _arrowExtraDamage, DamageType.PhysDamage);
            }
        }
        if (other.TryGetComponent(out DestroyShards _destroyShards))
        {
            _destroyShards.ShardsDestroy();
            transform.SetParent(other.transform);
        }
        _lifeTime += 60f;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.isKinematic = true;
        _collider.enabled = false;
        Destroy(gameObject, _lifeTime);
    }

    private IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}

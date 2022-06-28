using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDamagePerSecond : MonoBehaviour
{
    public bool _canDamage = true;
    private float _defaultDamageTime = 1;
    private float _currentDamageTime;

    private void Start()
    {
        ResetTimer();
    }
    private void OnTriggerStay(Collider collision)
    {
        var rnd = Random.Range(1, 100);
        if (collision.TryGetComponent(out EnemyStats enemyStats) && _canDamage)
        {
            _canDamage = false;
            if (rnd < CurrentDamage.CurrentDamageS.CurrentMageCritChance)
            {
                enemyStats.CalculateDamage(CurrentDamage.CurrentDamageS.CurrentWeaponMagicDamage * 1f, DamageType.MageDamage, true);
            }
            else
            {
                enemyStats.CalculateDamage(CurrentDamage.CurrentDamageS.CurrentWeaponMagicDamage * 0.5f, DamageType.MageDamage, false);
            }
        }
    }

    private void Update()
    {
        if (!_canDamage)
        {
            _currentDamageTime -= Time.deltaTime;
            if (_currentDamageTime <= 0)
            {
                _canDamage = true;
                ResetTimer();
            }
        }
    }

    private void ResetTimer()
    {
        _currentDamageTime = _defaultDamageTime;
    }
}

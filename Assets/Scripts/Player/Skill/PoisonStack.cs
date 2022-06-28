using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonStack : MonoBehaviour
{
    [SerializeField] private int _poisonStack;
    [SerializeField] private float _timeToStartDamage;
    private Collider _damageCollider;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_timeToStartDamage);
        _damageCollider = GetComponent<Collider>();
        _damageCollider.enabled = true;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent(out EnemyDotStatus enemyDotStatus))
        {
            enemyDotStatus.TakePosionDamage(_poisonStack);
        }
    }
}

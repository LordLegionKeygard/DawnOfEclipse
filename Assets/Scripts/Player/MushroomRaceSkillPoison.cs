using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomRaceSkillPoison : MonoBehaviour
{
    [SerializeField] private int _poisonStack;
    private Collider _damageCollider;

    private void OnEnable()
    {
        _damageCollider = GetComponent<Collider>();
        Invoke("ColliderActive", 0.5f);
    }

    private void ColliderActive()
    {
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

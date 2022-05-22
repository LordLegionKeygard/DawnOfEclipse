using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomRaceSkillPoison : MonoBehaviour
{
    [SerializeField] private int _poisonStack;
    private Collider _damageCollider;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        _damageCollider = GetComponent<Collider>();
        _damageCollider.enabled = true;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent(out EnemyDotStatus enemyDotStatus))
        {
            Debug.Log("Collision");
            enemyDotStatus.TakePosionDamage(_poisonStack);
        }
    }
}

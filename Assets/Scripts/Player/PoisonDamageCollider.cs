using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDamageCollider : MonoBehaviour
{
    [SerializeField] private ParticleSystem _posionHandPS;
    [SerializeField] private int _poisonStack;
    private Collider _damageCollider;
    public bool CanDamage = true;

    private bool _poisonParticle;

    private void OnEnable()
    {
        _damageCollider = GetComponent<Collider>();
        CustomEvents.OnEnabledDamageCollider += StackPoison;
        CustomEvents.OnPoisonHandsParticle += PoisonPsToggle;
    }

    private void PoisonPsToggle(bool state)
    {
        if (!CanDamage) return;

        if (state)
        {
            _poisonParticle = true;
            _posionHandPS.Play();
        }
        else
        {
            _poisonParticle = false;
            StartCoroutine(ExecuteAfterTime(0.5f));
            IEnumerator ExecuteAfterTime(float timeInSec)
            {
                yield return new WaitForSeconds(timeInSec);
                if (!_poisonParticle) _posionHandPS.Stop();
            }
        }
    }

    public void StackPoison(bool state)
    {
        _damageCollider.enabled = state;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent(out EnemyDotStatus enemyDotStatus) && CanDamage)
        {
            enemyDotStatus.TakePosionDamage(_poisonStack);
            StackPoison(false);
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnEnabledDamageCollider -= StackPoison;
        CustomEvents.OnPoisonHandsParticle -= PoisonPsToggle;
    }
}

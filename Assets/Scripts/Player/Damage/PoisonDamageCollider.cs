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
        CustomEvents.OnPoisonHandsParticle += PoisonPsToggle;
    }

    private void PoisonPsToggle(bool state)
    {
        if (!CanDamage || CharacterInformation.Race != 1) return;

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

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent(out EnemyTakeDotStatus enemyTakeDotStatus) && CanDamage)
        {
            if (CharacterInformation.Race != 1) return;
            enemyTakeDotStatus.EnemyDot.TakePosionDamage(_poisonStack);
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnPoisonHandsParticle -= PoisonPsToggle;
    }
}

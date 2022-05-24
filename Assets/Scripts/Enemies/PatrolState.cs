using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : NewState
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private NewIdleState _newIdleState;
    public Transform _rndPatrolTransform;
    public Transform SpawnPosition;

    [SerializeField] private float _patrolRadius;

    [SerializeField] private int _patrolAnimLength;

    public override NewState Tick(NewEnemyManager newEnemyManager, EnemyStats enemyStats, NewEnemyAnimatorManager newEnemyAnimatorManager)
    {

        # region Attack #
        if (enemyStats.Aggression)
        {
            _newIdleState.IsAttack = true;
            return _newIdleState;
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, _newIdleState.CurrentDetectionRadius, _newIdleState.DetectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            HealthControl playerHealthControl = colliders[i].transform.GetComponent<HealthControl>();

            if (playerHealthControl != null && playerHealthControl.CurrentHealth > 0)
            {
                Vector3 targetDirection = playerHealthControl.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if (viewableAngle > CharacterManager.MinimumDetectionAngle && viewableAngle < CharacterManager.MaximumDetectionAngle)
                {
                    return _newIdleState;
                }
            }
        }

        #endregion #


        return this;
    }

    public void PatrolToRandomPosition()
    {
        var x = Random.Range(SpawnPosition.position.x - _patrolRadius, SpawnPosition.position.x + _patrolRadius);
        var y = transform.position.y;
        var z = Random.Range(SpawnPosition.position.z - _patrolRadius, SpawnPosition.position.z + _patrolRadius);

        _rndPatrolTransform.position = new Vector3(x, y, z);
        _rndPatrolTransform.parent = null;
        _aiDestinationSetter.CurrentTarget = _rndPatrolTransform;
    }

    public IEnumerator RandomAction()
    {
        var random = Random.Range(1, 3);
        yield return new WaitForSeconds(random);
        var rnd = Random.Range(1, _patrolAnimLength + 1);

        switch (rnd)
        {
            case 1:
                _animator.SetTrigger("p1");
                break;
            case 2:
                _animator.SetTrigger("p2");
                break;
            case 3:
                _animator.SetTrigger("p3");
                break;
            case 4:
                _animator.SetTrigger("p4");
                break;
            case 5:
                _animator.SetTrigger("p5");
                break;
        }

    }

    public void TimerToNewPatrolAction()
    {
        var rnd = Random.Range(5, 30);

        Invoke("PatrolToRandomPosition", rnd);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : NewState
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private NewIdleState _newIdleState;

    public Transform _rndPatrolTransform;

    public override NewState Tick(NewEnemyManager newEnemyManager, EnemyStats enemyStats, NewEnemyAnimatorManager newEnemyAnimatorManager)
    {

        # region Attack #
        if (enemyStats.Aggression)
        {
            _newIdleState.IsAttack = true;
            return _newIdleState;
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, _newIdleState.DetectionRadius, _newIdleState.DetectionLayer);

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

        # endregion #

        
        return this;
    }

    public void PatrolToRandomPosition()
    {
        var x = Random.Range(transform.position.x - 10, transform.position.x + 10);
        var y = transform.position.y;
        var z = Random.Range(transform.position.z - 10, transform.position.z + 10);

        _rndPatrolTransform.position = new Vector3(x, y, z);
        _rndPatrolTransform.parent = null;
        _aiDestinationSetter.CurrentTarget = _rndPatrolTransform;
    }

    public void RandomAction()
    {
        Debug.Log("11");
        _animator.SetTrigger("1");
    }
}

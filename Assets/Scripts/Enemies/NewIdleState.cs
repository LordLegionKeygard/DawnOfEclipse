using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NewIdleState : NewState
{
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private NewPursueTargetState _newPursueTargetState;
    public LayerMask detectionLayer;
    public float DetectionRadius;

    public override NewState Tick(NewEnemyManager newEnemyManager, EnemyStats enemyStats, NewEnemyAnimatorManager newEnemyAnimatorManager)
    {
        if (!enemyStats.Aggression) DetectionRadius = CharacterManager.DefaultDetectionRadius;
        if (enemyStats.Aggression)
        {
            DetectionRadius = 100;
        }
        Collider[] colliders = Physics.OverlapSphere(transform.position, DetectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            HealthControl playerHealthControl = colliders[i].transform.GetComponent<HealthControl>();

            if (playerHealthControl != null && playerHealthControl.CurrentHealth > 0)
            {
                Vector3 targetDirection = playerHealthControl.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if (viewableAngle > CharacterManager.MinimumDetectionAngle && viewableAngle < CharacterManager.MaximumDetectionAngle)
                {
                    _aiDestinationSetter.CurrentTarget = playerHealthControl.gameObject.transform;
                }
            }
        }

        if (_aiDestinationSetter.CurrentTarget != null)
        {
            return _newPursueTargetState;
        }
        else
        {
            return this;
        }
    }
}

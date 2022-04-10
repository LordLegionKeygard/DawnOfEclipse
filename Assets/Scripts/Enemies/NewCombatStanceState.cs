using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NewCombatStanceState : NewState
{
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private NewAttackState _newAttackState;
    [SerializeField] private NewPursueTargetState _newPursueTargetState;
    public NewIdleState NewIdleState;

    public override NewState Tick(NewEnemyManager newEnemyManager, EnemyStats enemyStats, NewEnemyAnimatorManager newEnemyAnimatorManager)
    {
        if (_aiDestinationSetter.CurrentTarget != null)
        {
            float distanceFromTarget = Vector3.Distance(_aiDestinationSetter.CurrentTarget.transform.position, newEnemyAnimatorManager.transform.position);

            if (newEnemyManager.currentRecoveryTime <= 0 && distanceFromTarget <= newEnemyManager.maximumAttackRange)
            {
                return _newAttackState;
            }
            else if (distanceFromTarget > newEnemyManager.maximumAttackRange)
            {
                return _newPursueTargetState;
            }
            else
            {
                return this;
            }
        }
        else
        {
            return NewIdleState;
        }
    }
}

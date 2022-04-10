using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NewPursueTargetState : NewState
{
    [SerializeField] private NewCombatStanceState _newCombatStanceState;
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private NewIdleState _newIdleState;
    public override NewState Tick(NewEnemyManager newEnemyManager, EnemyStats enemyStats, NewEnemyAnimatorManager newEnemyAnimatorManager)
    {
        if (_aiDestinationSetter.CurrentTarget != null)
        {
            float distanceFromTarget = Vector3.Distance(_aiDestinationSetter.CurrentTarget.transform.position, newEnemyManager.transform.position);

            newEnemyManager.IsChasingPlayer = true;

            if (distanceFromTarget > 20f)
            {
                newEnemyManager.ReturnToSpawn();
            }

            if (distanceFromTarget > newEnemyManager.maximumAttackRange)
            {
                newEnemyManager.IsChasingPlayer = true;
            }

            if (distanceFromTarget <= newEnemyManager.maximumAttackRange)
            {
                newEnemyManager.IsChasingPlayer = false;
                return _newCombatStanceState;
            }
            else
            {
                return this;
            }
        }
        else
        {
            return _newIdleState;
        }
    }
}

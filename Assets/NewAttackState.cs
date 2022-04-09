using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class NewAttackState : NewState
{
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private NewCombatStanceState _newCombatStanceState;
    [SerializeField] private EnemyAttackAction currentAttack;
    public EnemyAttackAction[] enemyAttacks;
    public override NewState Tick(NewEnemyManager newEnemyManager, EnemyStats enemyStats, NewEnemyAnimatorManager newEnemyAnimatorManager)
    {
        float distanceFromTarget = Vector3.Distance(_aiDestinationSetter.CurrentTarget.transform.position, newEnemyManager.transform.position);

        if (!newEnemyManager.isCanAttack)
            return _newCombatStanceState;

        if (currentAttack != null)
        {
            if (distanceFromTarget < currentAttack.minimumDistanceNeededToAttack)
            {
                return this;
            }
            else if (distanceFromTarget < currentAttack.maximumDistanceNeededToAttack)
            {
                {
                    if (newEnemyManager.currentRecoveryTime <= 0 && newEnemyManager.isCanAttack)
                    {
                        //остановить персонажа
                        newEnemyManager.isCanAttack = false;
                        newEnemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                        currentAttack = null;
                        return _newCombatStanceState;
                    }
                }
            }
        }
        else
        {
            GetNewAttack(newEnemyManager);
        }

        return _newCombatStanceState;
    }
    private void GetNewAttack(NewEnemyManager newEnemyManager)
    {
        newEnemyManager.ResetChaseTimer();
        float distanceFromTarget = Vector3.Distance(_aiDestinationSetter.CurrentTarget.transform.position, transform.position);

        int maxScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                {
                    maxScore += enemyAttackAction.attackScore;
                }
            }
        }

        int randomValue = Random.Range(0, maxScore);
        int temporaryScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                {
                    if (currentAttack != null)
                        return;

                    temporaryScore += enemyAttackAction.attackScore;

                    if (temporaryScore > randomValue)
                    {
                        currentAttack = enemyAttackAction;
                    }
                }
            }
        }
    }
}

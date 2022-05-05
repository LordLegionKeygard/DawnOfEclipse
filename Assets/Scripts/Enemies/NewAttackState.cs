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
        Vector3 targetDirection = _aiDestinationSetter.CurrentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        float distanceFromTarget = Vector3.Distance(_aiDestinationSetter.CurrentTarget.transform.position, newEnemyManager.transform.position);

        if (!newEnemyManager.IsCanAttack)
            return _newCombatStanceState;

        if (currentAttack != null)
        {
            if (distanceFromTarget < currentAttack.MinimumDistanceNeededToAttack)
            {
                return this;
            }
            else if (distanceFromTarget < currentAttack.MaximumDistanceNeededToAttack)
            {
                if (viewableAngle <= currentAttack.MaximumAttackAngle &&
                    viewableAngle >= currentAttack.MinimumAttackAngle)
                {

                    if (newEnemyManager.currentRecoveryTime <= 0 && newEnemyManager.IsCanAttack)
                    {
                        newEnemyAnimatorManager.PlayerTargetAnimation(currentAttack.actionAnimation);
                        newEnemyManager.IsCanAttack = false;
                        newEnemyManager.currentRecoveryTime = currentAttack.RecoveryTime;
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
        Vector3 targetsDirection = _aiDestinationSetter.CurrentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);
        float distanceFromTarget = Vector3.Distance(_aiDestinationSetter.CurrentTarget.transform.position, transform.position);

        int maxScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (distanceFromTarget <= enemyAttackAction.MaximumDistanceNeededToAttack
                && distanceFromTarget >= enemyAttackAction.MinimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.MaximumAttackAngle
                    && viewableAngle >= enemyAttackAction.MinimumAttackAngle)
                {
                    maxScore += enemyAttackAction.AttackScore;
                }
            }
        }

        int randomValue = Random.Range(0, maxScore);
        int temporaryScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (distanceFromTarget <= enemyAttackAction.MaximumDistanceNeededToAttack
                && distanceFromTarget >= enemyAttackAction.MinimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.MaximumAttackAngle
                    && viewableAngle >= enemyAttackAction.MinimumAttackAngle)
                {
                    if (currentAttack != null)
                        return;

                    temporaryScore += enemyAttackAction.AttackScore;

                    if (temporaryScore > randomValue)
                    {
                        currentAttack = enemyAttackAction;
                    }
                }
            }
        }
    }
}

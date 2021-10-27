using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardTargetState : State
{
    public PursueTargetState pursueTargetState;

    public IdleState idleState;
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyAnimatorManager.anim.SetFloat("Vertical", 0);
        enemyAnimatorManager.anim.SetFloat("Horizontal", 0);

        if (enemyManager.currentTarget != null)
        {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);

            if (viewableAngle >= 100 && viewableAngle <= 180 && !enemyManager.isInteracting)
            {
                enemyAnimatorManager.PlayerTargetAnimationWithRootRotation("Sword and Shiled 180 Turn", true);
                return pursueTargetState;
            }
            else if (viewableAngle <= -101 && viewableAngle >= -180 && !enemyManager.isInteracting)
            {
                enemyAnimatorManager.PlayerTargetAnimationWithRootRotation("Sword and Shiled 180 Turn", true);
                return pursueTargetState;
            }
            else if (viewableAngle <= -45 && viewableAngle >= -100 && !enemyManager.isInteracting)
            {
                enemyAnimatorManager.PlayerTargetAnimationWithRootRotation("Turn Right", true);
                return pursueTargetState;
            }
            else if (viewableAngle >= 45 && viewableAngle <= 100 && !enemyManager.isInteracting)
            {
                enemyAnimatorManager.PlayerTargetAnimationWithRootRotation("Turn Left", true);
                return pursueTargetState;
            }
            return pursueTargetState;
        }

        return idleState;
    }
}
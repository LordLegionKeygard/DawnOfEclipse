using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushState : MonoBehaviour
{
    // public bool isSleeping;
    // public float detectionRadius = 2f;
    // public string sleepAnimation;
    // public string wakeAnimation;
    // public LayerMask detectionLayer;

    // public PursueTargetState pursueTargetState;
    // public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    // {
    //     if (isSleeping && enemyManager.isInteracting == false)
    //     {
    //         enemyAnimatorManager.PlayerTargetAnimation(sleepAnimation, true);
    //     }

    //     #region Handle Target Detection

    //     Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, detectionRadius, detectionLayer);

    //     for (int i = 0; i < colliders.Length; i++)
    //     {
    //         CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

    //         if (characterStats != null)
    //         {
    //             Vector3 targetsDirection = characterStats.transform.position - enemyManager.transform.position;
    //             float viewableAngle = Vector3.Angle(targetsDirection, enemyManager.transform.forward);

    //             if (viewableAngle > EnemyManager.minimumDetectionAngle
    //                 && viewableAngle < EnemyManager.maximumDetectionAngle)
    //             {
    //                 enemyManager.currentTarget = characterStats.gameObject;
    //                 isSleeping = false;
    //                 enemyAnimatorManager.PlayerTargetAnimation(wakeAnimation, true);
    //             }
    //         }
    //     }
    //     #endregion

    //     #region Handle State Change

    //     if (enemyManager.currentTarget != null)
    //     {
    //         return pursueTargetState;
    //     }
    //     else
    //     {
    //         return this;
    //     }

    //     #endregion
    // }
}
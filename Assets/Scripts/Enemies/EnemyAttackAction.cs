using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/Enemy Actions/Attack Action")]
public class EnemyAttackAction : EnemyAction
{
    public int AttackScore = 3;
    public float RecoveryTime = 2;

    public float MinimumDistanceNeededToAttack = 0;
    public float MaximumDistanceNeededToAttack = 1.5f;
}

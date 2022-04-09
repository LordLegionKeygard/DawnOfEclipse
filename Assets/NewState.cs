using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NewState : MonoBehaviour
{
    public abstract NewState Tick(NewEnemyManager newEnemyManager, EnemyStats enemyStats, NewEnemyAnimatorManager newEnemyAnimatorManager);
}

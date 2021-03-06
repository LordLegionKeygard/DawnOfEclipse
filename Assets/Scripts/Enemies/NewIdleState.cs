using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NewIdleState : NewState
{
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private NewPursueTargetState _newPursueTargetState;
    [SerializeField] private PatrolState _patrolState;
    public LayerMask DetectionLayer;
    public float CurrentDetectionRadius;
    public float DefaultDetectionRadius;
    private float _currentTimer = 10;
    public bool IsPatrol;
    public bool IsAttack;

    public override NewState Tick(NewEnemyManager newEnemyManager, EnemyStats enemyStats, NewEnemyAnimatorManager newEnemyAnimatorManager)
    {
        if (!enemyStats.Aggression)
        {
            IsAttack = false;
            CurrentDetectionRadius = DefaultDetectionRadius;
            newEnemyAnimatorManager.CombatBoolAnimation(false);
        }

        if (enemyStats.Aggression)
        {
            CurrentDetectionRadius = 100;
            IsAttack = true;
            IsPatrol = false;
            newEnemyAnimatorManager.CombatBoolAnimation(true);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, CurrentDetectionRadius, DetectionLayer);

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
            IsAttack = true;
            IsPatrol = false;
            _currentTimer = 10;
            return _newPursueTargetState;
        }

        if (IsPatrol && !IsAttack)
        {
            if (_patrolState == null) { return this; }
            _patrolState.PatrolToRandomPosition();
            return _patrolState;
        }

        else
        {
            return this;
        }
    }

    private void Update()
    {
        if (IsAttack) { return; }
        _currentTimer -= Time.deltaTime;
        if (_currentTimer <= 0)
        {
            IsPatrol = true;
        }
    }
}

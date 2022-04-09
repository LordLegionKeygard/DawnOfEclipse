using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NewEnemyManager : CharacterManager
{
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    private EnemyStats _enemyStats;
    [SerializeField] private NewEnemyAnimatorManager _newEnemyAnimatorManager;
    [SerializeField] private MobSpawner _spawnPoint;
    public NewState CurrentState;
    private float _timeToChase = 15f;
    private float _chaseTime;
    public bool IsChasingPlayer = false;
    public bool isCanAttack = true;

    private void Awake()
    {
        _enemyStats = GetComponent<EnemyStats>();
    }

    private void Start()
    {
        HealthControl.PlayerDeathEvent += ReturnToSpawn;
        _spawnPoint = GetComponentInParent<MobSpawner>();
    }

    private void Update()
    {
        HandleRecoveryTime();

        if (IsChasingPlayer)
        {
            StartChasingTimer();
        }
    }
    private void FixedUpdate()
    {
        HandleStateMachine();

        if (_aiDestinationSetter.CurrentTarget != null)
        {
            float distanceFromTarget = Vector3.Distance(_aiDestinationSetter.CurrentTarget.transform.position, transform.position);
            if (_aiDestinationSetter.CurrentTarget == _spawnPoint.gameObject.transform && distanceFromTarget < 3)
            {
                _aiDestinationSetter.CurrentTarget = null;
                _newEnemyAnimatorManager.IsAtDestination = false;
            }
        }
    }

    private void HandleStateMachine()
    {
        if (CurrentState != null)
        {
            NewState nextState = CurrentState.Tick(this, _enemyStats, _newEnemyAnimatorManager);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(NewState state)
    {
        CurrentState = state;
    }

    private void HandleRecoveryTime()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if (isCanAttack)
        {
            if (currentRecoveryTime <= 0)
            {
                isCanAttack = false;
            }
        }
    }

    public void ReturnToSpawn()
    {
        IsChasingPlayer = false;
        ResetChaseTimer();
        _aiDestinationSetter.CurrentTarget = _spawnPoint.gameObject.transform;
    }

    private void StartChasingTimer()
    {
        _chaseTime -= Time.deltaTime;

        if (_chaseTime < 0f)
        {
            ReturnToSpawn();
        }
    }
    public void ResetChaseTimer()
    {
        _chaseTime = _timeToChase;
    }

    private void OnDisable()
    {
        HealthControl.PlayerDeathEvent -= ReturnToSpawn;
    }
}

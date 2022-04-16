using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NewEnemyManager : CharacterManager
{
    public float maximumAttackRange;
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    private EnemyStats _enemyStats;
    private NewEnemyAnimatorManager _newEnemyAnimatorManager;
    [SerializeField] private MobSpawner _spawnPoint;
    [SerializeField] private NewState _currentState;
    [SerializeField] private float _timeToChase = 15f;
    public float _chaseTime;
    public bool IsChasingPlayer = false;
    public bool IsCanAttack = true;

    private void Awake()
    {
        _enemyStats = GetComponent<EnemyStats>();
        _newEnemyAnimatorManager = GetComponent<NewEnemyAnimatorManager>();
    }

    private void Start()
    {
        _chaseTime = _timeToChase;
        CustomEvents.OnPlayerDeath += ReturnToSpawn;
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
            }
        }
    }

    private void HandleStateMachine()
    {
        if (_currentState != null)
        {
            NewState nextState = _currentState.Tick(this, _enemyStats, _newEnemyAnimatorManager);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(NewState state)
    {
        _currentState = state;
    }

    private void HandleRecoveryTime()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if (!IsCanAttack)
        {
            if (currentRecoveryTime <= 0)
            {
                IsCanAttack = true;
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

        if (_chaseTime < 0)
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
        CustomEvents.OnPlayerDeath -= ReturnToSpawn;
    }
}

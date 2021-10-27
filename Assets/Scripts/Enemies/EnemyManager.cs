using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{
    private EnemyLocomotionManager enemyLocomotionManager;
    private EnemyAnimatorManager enemyAnimationManager;
    private EnemyStats enemyStats;
    [SerializeField] private MobSpawner spawnPoint;
    private float timeToChase = 15f;
    private float chaseTime;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Rigidbody enemyRigidBody;
    [HideInInspector] public bool isPerformingAction;
    [HideInInspector] public bool isInteracting;
    [HideInInspector] public float rotationSpeed = 15;
    [HideInInspector] public bool isChasingPlayer = false;
    [HideInInspector] public float maximumAttackRange = 1.5f;
    public State currentState;
    public GameObject currentTarget;

    private void Awake()
    {
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        enemyAnimationManager = GetComponentInChildren<EnemyAnimatorManager>();
        enemyStats = GetComponent<EnemyStats>();
        enemyRigidBody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();       
        navMeshAgent.enabled = false;
    }

    private void Start()
    {
        float randomY = Random.Range(0, 180);
        enemyRigidBody.isKinematic = false;
        transform.Rotate(0f, randomY, 0.0f, Space.World);
        spawnPoint = GetComponentInParent<MobSpawner>();
        chaseTime = timeToChase;
    }

    private void Update()
    {
        HandleRecoveryTime();

        isRotatingWithRootMotion = enemyAnimationManager.anim.GetBool("isRotatingWithRootMotion");

        isInteracting = enemyAnimationManager.anim.GetBool("isInteracting");

        if (isChasingPlayer)
        {
            StartChasingTimer();
        }

    }
    private void FixedUpdate()
    {
        HandleStateMachine();

        if (currentTarget != null)
        {
            float distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
            if (currentTarget == spawnPoint.gameObject && distanceFromTarget < 3 && currentTarget != null)
            {
                currentTarget = null;
                navMeshAgent.enabled = false;
                enemyAnimationManager.anim.SetFloat("Vertical", 0, 0f, Time.deltaTime);
            }
        }
    }

    private void HandleStateMachine()
    {
        if (currentState != null)
        {
            State nextState = currentState.Tick(this, enemyStats, enemyAnimationManager);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(State state)
    {
        currentState = state;
    }

    private void HandleRecoveryTime()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if (isPerformingAction)
        {
            if (currentRecoveryTime <= 0)
            {
                isPerformingAction = false;
            }
        }
    }

    public void ReturnToSpawn()
    {
        isChasingPlayer = false;
        ResetChaseTimer();
        currentTarget = spawnPoint.gameObject;
    }

    private void StartChasingTimer()
    {
        chaseTime -= Time.deltaTime;

        if (chaseTime < 0f)
        {
            ReturnToSpawn();
        }
    }
    public void ResetChaseTimer()
    {
        chaseTime = timeToChase;
    }
}
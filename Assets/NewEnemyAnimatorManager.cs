using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NewEnemyAnimatorManager : VersionedMonoBehaviour
{
    [SerializeField] private NewEnemyManager _newEnemyManager;
    public Animator Animator;
    public bool IsAtDestination;
    private IAstarAI _ai;
    private Transform _transform;

    protected override void Awake()
    {
        base.Awake();
        _ai = GetComponent<IAstarAI>();
        _transform = GetComponent<Transform>();
    }
    protected void Update()
    {
        if (_newEnemyManager.IsChasingPlayer == true)
            if (_ai.reachedEndOfPath)
            {
                IsAtDestination = true;
                Animator.SetFloat("speed", 0, 0.5f, Time.deltaTime);
            }
            else
            {
                IsAtDestination = false;
                Vector3 relVelocity = _transform.InverseTransformDirection(_ai.velocity);
                relVelocity.y = 0;
                Animator.SetFloat("speed", relVelocity.magnitude / Animator.transform.lossyScale.x, 0.5f, Time.deltaTime);
            }
    }
}

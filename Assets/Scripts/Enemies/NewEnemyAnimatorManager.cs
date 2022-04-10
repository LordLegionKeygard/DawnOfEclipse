using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NewEnemyAnimatorManager : VersionedMonoBehaviour
{
    [SerializeField] private Collider _damageCollider;
    private Animator _animator;
    private IAstarAI _ai;
    private Transform _transform;


    protected override void Awake()
    {
        base.Awake();
        _ai = GetComponent<IAstarAI>();
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
    }

    public void PlayerTargetAnimation(string targetAnim)
    {
        _animator.SetTrigger(targetAnim.ToString());
    }
    protected void Update()
    {
        if (_ai.reachedEndOfPath)
        {
            _animator.SetFloat("speed", 0, 0.5f, Time.deltaTime);
        }
        else
        {
            Vector3 relVelocity = _transform.InverseTransformDirection(_ai.velocity);
            relVelocity.y = 0;
            _animator.SetFloat("speed", relVelocity.magnitude / _animator.transform.lossyScale.x, 0.5f, Time.deltaTime);
        }
    }

    public void EnableDamageCollider()
    {
        _damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        _damageCollider.enabled = false;
    }
}

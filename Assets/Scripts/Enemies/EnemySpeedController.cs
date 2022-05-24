using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySpeedController : MonoBehaviour
{
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _walkSpeed;
    private AIPath _aiPath;

    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
    }

    public void CanWalk()
    {
        _aiPath.maxSpeed = _walkSpeed;
    }

    public void CanRun()
    {
        _aiPath.maxSpeed = _runSpeed;
    }

    public void CantWalk()
    {
        _aiPath.maxSpeed = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySpeedController : MonoBehaviour
{
    [SerializeField] private float _normalSpeed;
    [SerializeField] private AIPath _aiPath;

    public void CanWalk()
    {
        _aiPath.maxSpeed = _normalSpeed;
    }

    public void CantWalk()
    {
        _aiPath.maxSpeed = 0;
    }
}

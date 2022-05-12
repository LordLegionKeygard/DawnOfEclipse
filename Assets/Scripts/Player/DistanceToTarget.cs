using UnityEngine;
using System.Collections;

public class DistanceToTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _character;
    public float Distance;

    private bool _canUpdateDistance;
    private void OnEnable()
    {
        CustomEvents.OnActiveTargetSkill += ToggleUpdateDistance;
    }
    private void Update()
    {
        if (!_canUpdateDistance) return;
        Distance = Vector3.Distance(_target.position, _character.position);
    }

    private void ToggleUpdateDistance(bool state)
    {
        _canUpdateDistance = state;
    }
}
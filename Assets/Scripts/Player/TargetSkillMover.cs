using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSkillMover : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private GameObject _skillRound;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private Transform _target;
    private Camera cam;

    private bool _isActive;

    private void OnEnable()
    {
        CustomEvents.OnActiveTargetSkill += ActiveTargetSkill;
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {

        if (!_isActive) return;
        UpdateTargetPosition();
    }

    private void ActiveTargetSkill(bool state)
    {
        _skillRound.SetActive(state);
        _isActive = state;
    }

    private void UpdateTargetPosition()
    {
        Vector3 newPosition = Vector3.zero;
        RaycastHit hit;

        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, _maxDistance, _mask))
        {
            _target.position = Vector3.Lerp(_target.position, hit.point, 10f * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0))
        {
            CustomEvents.FireUseTargetSkill();
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnActiveTargetSkill -= ActiveTargetSkill;
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLockOnTarget : MonoBehaviour
{
    [SerializeField] private CinemachineInputProvider _cinemachineInputProvider;
    [SerializeField] private GameObject _cameraPivot;
    [SerializeField] private CinemachineFreeLook _lockCamera;
    private List<CharacterManager> _avilableTargets = new List<CharacterManager>();
    private Transform _myTransform;
    private Transform _currentLockOnTarget;
    private Transform _nearestLockOnTarget;
    private float _maximumLockOnDistance = 25;
    private bool _lockOnFlag;
    private Animator _animator;

    private void Start()
    {
        CustomEvents.OnCameraLockOnTarget += TargetLock;
        CustomEvents.OnCameraLockOnTargetDeath += TargetDeath;
        _myTransform = this.transform;
        _animator = GetComponent<Animator>();
    }

    public void TargetLock()
    {
        if (_lockOnFlag == false)
        {
            ClearLockOnTargets();
            _lockOnFlag = true;
            _currentLockOnTarget = _nearestLockOnTarget;
            HandleLockOn();

            if (_nearestLockOnTarget != null)
            {
                _currentLockOnTarget = _nearestLockOnTarget;
                _lockOnFlag = true;
            }
        }
        else if (_lockOnFlag)
        {
            _currentLockOnTarget = null;
            _nearestLockOnTarget = null;
            _lockCamera.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
            _cinemachineInputProvider.enabled = true;
            _cameraPivot.transform.parent = null;
            _lockOnFlag = false;
            _animator.SetBool("strafe", false);
        }
    }

    private void ClearLockOnTargets()
    {
        _avilableTargets.Clear();
        _nearestLockOnTarget = null;
        _currentLockOnTarget = null;
    }

    public void TargetDeath()
    {
        ClearLockOnTargets();
        _lockCamera.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
        _cinemachineInputProvider.enabled = true;
        _cameraPivot.transform.parent = null;
        _lockOnFlag = false;
        _animator.SetBool("strafe", false);
    }
    private void LateUpdate()
    {
        if (_currentLockOnTarget != null)
        {
            _animator.SetBool("strafe", true);
            _lockCamera.m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetWithWorldUp;
            Vector3 dir = _currentLockOnTarget.position - _myTransform.position;
            dir.Normalize();
            dir.y = 0;
            _myTransform.rotation = Quaternion.LookRotation(dir);
            _cameraPivot.transform.rotation = Quaternion.Lerp(_myTransform.rotation, transform.rotation, Time.deltaTime / 10);
        }
    }

    public void HandleLockOn()
    {
        float shortestDistance = Mathf.Infinity;

        Collider[] colliders = Physics.OverlapSphere(_myTransform.position, 26);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager character = colliders[i].GetComponent<CharacterManager>();

            if (character != null)
            {
                Vector3 lockTargetDirection = character.transform.position - _myTransform.position;
                float distanceFromTarget = Vector3.Distance(_myTransform.position, character.transform.position);

                if (character.transform.root != _myTransform.transform.root

                    && distanceFromTarget <= _maximumLockOnDistance)
                {
                    _avilableTargets.Add(character);
                }
            }
        }

        for (int k = 0; k < _avilableTargets.Count; k++)
        {
            float distanceFromTarget = Vector3.Distance(_myTransform.position, _avilableTargets[k].transform.position);

            if (distanceFromTarget < shortestDistance)
            {
                shortestDistance = distanceFromTarget;
                _nearestLockOnTarget = _avilableTargets[k].LockOnTransform;
                _cameraPivot.transform.SetParent(this.gameObject.transform);
                _cinemachineInputProvider.enabled = false;
            }
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnCameraLockOnTarget -= TargetLock;
        CustomEvents.OnCameraLockOnTargetDeath -= TargetDeath;
    }
}

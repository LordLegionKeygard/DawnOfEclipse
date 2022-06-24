using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 280f;
    private PlayerAnimatorManager _playerAnimatorManager;
    private PlayerInputController _playerInputController;
    private PotionsControl _potionsControl;
    private Animator _animator;
    private CharacterController _characterController;
    public float CurrentSpeed;
    public float DefaultSpeed;
    private float _velocityMove;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float deceleration = 0.5f;
    private Vector2 _inputStrafe;
    public bool _canWalk;
    private bool _canRotate = true;
    private bool _isDeath;

    private void OnEnable()
    {
        CustomEvents.OnCanWalk += CanWalkToggle;
        CustomEvents.OnCanRotate += CanRotateToggle;
    }

    public void CalculateSpeed()
    {
        CurrentSpeed = DefaultSpeed;
    }

    private void Awake()
    {
        _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        _characterController = GetComponent<CharacterController>();
        _potionsControl = GetComponent<PotionsControl>();
        _playerInputController = GetComponent<PlayerInputController>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (HealthControl.IsDeath) return;

        if (_playerInputController.IsRoll) return;

        _inputStrafe.x = Input.GetAxis("Horizontal");
        _inputStrafe.y = Input.GetAxis("Vertical");

        _animator.SetFloat("inputX", _inputStrafe.x, 0.3f, Time.deltaTime * 10f);
        _animator.SetFloat("inputY", _inputStrafe.y, 0.3f, Time.deltaTime * 10f);

        Walk();
    }

    private void Walk()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.5f)
        {
            Vector3 projectedCameraForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
            Quaternion rotationToCamera = Quaternion.LookRotation(projectedCameraForward, Vector3.up);

            Vector3 moveDirection = rotationToCamera * direction;
            Quaternion rotationToMoveDirection = Quaternion.LookRotation(moveDirection, Vector3.up);

            if(!_canRotate) return;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToMoveDirection, _rotationSpeed * Time.deltaTime);

            if (!_canWalk) return;

            _characterController.Move(moveDirection.normalized * (CurrentSpeed + _potionsControl.PotionSpeed) * Time.deltaTime);

            if (_velocityMove < 1.0f)
            {
                _velocityMove += Time.deltaTime * acceleration;
            }
        }
        else
        {
            if (_velocityMove > 0.0f)
            {
                _velocityMove -= Time.deltaTime * deceleration;
            }
        }
        _animator.SetFloat(_playerAnimatorManager.Speed, _velocityMove);
    }

    private void CanWalkToggle(bool state)
    {
        _canWalk = state;
    }

    private void CanRotateToggle(bool state)
    {
        _canRotate = state;
    }

    private void OnDisable()
    {
        CustomEvents.OnCanWalk -= CanWalkToggle;
        CustomEvents.OnCanRotate -= CanRotateToggle;
    }
}

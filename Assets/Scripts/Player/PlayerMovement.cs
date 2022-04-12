using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : CharacterManager
{
    private PlayerInputController _playerInputController;
    private PotionsControl _potionsControl;
    private PlayerAnimatorManager _playerAnimatorManager;
    private Animator _animator;
    private CharacterController _characterController;
    [SerializeField] private Transform _camera;
    public float CurrentSpeed;
    public float DefaultSpeed = 5f;
    [SerializeField] private float turnSmothTime;
    [SerializeField] private float turnSmoothVelocity;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float deceleration = 0.5f;
    private Vector2 _inputStrafe;
    public bool CanWalk;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _potionsControl = GetComponent<PotionsControl>();
        _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        _playerInputController = GetComponent<PlayerInputController>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (HealthControl.IsDeath) return;

        if (_playerInputController.IsRoll) return;

        _inputStrafe.x = Input.GetAxis("Horizontal");
        _inputStrafe.y = Input.GetAxis("Vertical");

        _animator.SetFloat("inputX", _inputStrafe.x);
        _animator.SetFloat("inputY", _inputStrafe.y);

        if(CanWalk) Walk();

    }

    private void Walk()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.5f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _characterController.Move(moveDir.normalized * (CurrentSpeed + _potionsControl.PotionSpeed) * Time.deltaTime);

            if (_playerAnimatorManager.VelocityMove < 1.0f)
            {
                _playerAnimatorManager.VelocityMove += Time.deltaTime * acceleration;
            }
        }
        else
        {
            if (_playerAnimatorManager.VelocityMove > 0.0f)
            {
                _playerAnimatorManager.VelocityMove -= Time.deltaTime * deceleration;
            }
        }
        _playerAnimatorManager.Speeding();
    }
}

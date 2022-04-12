using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private CharacterController _characterController;
    private PotionsControl _potionsControl;
    private PlayerAnimatorManager _playerAnimatorManager;
    private StaminaControl _staminaControl;
    private PlayerInput _input;

    [Header("Jump")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    private float _gravity = -20f;
    private float _groundDistance = 1;
    private Vector3 _velocity;
    private float _velocityUpdateY = -10;
    private float _jumpHeight = 1.5f;

    [Header("Bool")]
    private bool _inputAttackR1;
    private bool _inputAttackR2;
    public bool IsRoll;
    private bool _isSneak;
    public bool IsFastRun;
    public bool IsAttack;
    public bool IsBlock;
    public bool IsCanJump = true;
    private bool _isGround;

    [Header("Stamina")]
    public float StaminaForR1 = 50f;
    public float StaminaForR2 = 50f;

    [Header("Time")]
    public float TimeR1;
    public float TimeR1FastRun;
    public float TimeR2;
    public float TimeL1;


    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.TargetLock.performed += ctx => CustomEvents.FireCameraLockOnTarget();
        _input.Player.Sneak.performed += ctx => Sneaking();
        _input.Player.BlockL1.performed += ctx => Block(true);
        _input.Player.BlockL1.canceled += ctx => Block(false);
        _input.Player.Jump.performed += ctx => Jump();
        _input.Player.AttackR1.performed += ctx => _inputAttackR1 = true;
        _input.Player.AttackR1.canceled += ctx => _inputAttackR1 = false;
        _input.Player.AttackR2.performed += ctx => _inputAttackR2 = true;
        _input.Player.AttackR2.canceled += ctx => _inputAttackR2 = false;
        _input.Player.Run.performed += ctx => Run(true);
        _input.Player.Run.canceled += ctx => Run(false);
        _input.Player.Roll.canceled += ctx => Roll();
    }

    private void Start()
    {
        _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        _playerMovement = GetComponent<PlayerMovement>();
        _characterController = GetComponent<CharacterController>();
        _potionsControl = GetComponent<PotionsControl>();
        _staminaControl = GetComponent<StaminaControl>();

    }
    private void Update()
    {
        if (HealthControl.IsDeath) return;

        if (_inputAttackR1) Attack(StaminaForR1, 1);
        if (_inputAttackR2) Attack(StaminaForR2, 2);

        if (_staminaControl.CurrentStamina <= 50)
        {
            _playerAnimatorManager.DisableBlockL1();
        }
        if (!_isSneak && !_potionsControl.SpeedPotion && !IsFastRun)
        {
            _staminaControl.StaminaRun = false;
            _playerMovement.CurrentSpeed = _playerMovement.DefaultSpeed;
            _potionsControl.PotionSpeed = 0;
        }
        
        _isGround = Physics.CheckSphere(groundCheck.position, _groundDistance, groundMask);
        if (_isGround && _velocity.y < 0) _velocity.y = _velocityUpdateY;
        {
            _velocity.y += _gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }
        if (_isGround == false) { _playerAnimatorManager.InAir(); }
        if (_isGround) { _playerAnimatorManager.OnGround(); }
    }
    private void Block(bool isPressed)
    {
        if (_staminaControl.CurrentStamina > 50 && isPressed) { _playerAnimatorManager.BlockL1(); }
        else { _playerAnimatorManager.DisableBlockL1(); }
    }

    private void Jump()
    {
        if (_isGround && !_isSneak && !IsRoll && !IsAttack && IsCanJump && _staminaControl.CurrentStamina > 50)
        {
            _playerAnimatorManager.JumpTrigger();
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            IsCanJump = false;
            _staminaControl.UseStamina(50);
        }
    }

    private void Attack(float stamina, int attackNumber)
    {
        if (_staminaControl.CurrentStamina > stamina && !IsAttack && _isGround && !_isSneak)
        {
            switch (attackNumber)
            {
                case 1:
                    if (!IsFastRun) { _playerAnimatorManager.AttackR1(); }
                    else { _playerAnimatorManager.AttackR1FastRun(); }
                    StartCoroutine(ExecuteAfterTime(TimeR1, _playerAnimatorManager.DisableAttackR1));
                    break;
                case 2:
                    _playerAnimatorManager.AttackR2();
                    StartCoroutine(ExecuteAfterTime(TimeR2, _playerAnimatorManager.DisableAttackR2));
                    break;
            }
        }
    }

    private void Run(bool isPressed)
    {
        bool _canNewMove = !IsAttack && _isGround && _playerMovement.IsWalk && !_isSneak && !IsBlock;
        if (_staminaControl.CurrentStamina > 50 && _canNewMove && isPressed)
        {
            if (!_potionsControl.SpeedPotion)
            {
                _staminaControl.StaminaRun = true;
                _playerMovement.CurrentSpeed = 7;
            }
            else { _potionsControl.PotionSpeed = 5; }
            _playerAnimatorManager.Running();
            _playerAnimatorManager.EnableFastRun();
        }
        if (!isPressed)
        {
            _playerAnimatorManager.NotRunning();
            StartCoroutine(ExecuteAfterTime(0.3f, _playerAnimatorManager.DisableFastRun));
        }
    }

    private void Roll()
    {
        bool _canNewMove = !IsAttack && _isGround && _playerMovement.IsWalk && !_isSneak && !IsBlock;
        if (!IsFastRun && _staminaControl.CurrentStamina > 100 && _canNewMove)
        {
            _playerAnimatorManager.EnableRoll();
            StartCoroutine(ExecuteAfterTime(1.1f, _playerAnimatorManager.DisableRoll));
        }
    }

    private void Sneaking()
    {
        if (!_isGround) return;

        if (_isSneak == false)
        {
            Vector3 tempCenter = _characterController.center;
            tempCenter.y = _characterController.height / 2;
            _characterController.center = tempCenter;
            _playerAnimatorManager.Sneaking();
            _playerMovement.CurrentSpeed = 2;
            _isSneak = true;
            CharacterManager.maximumDetectionAngle = 50f;
            CharacterManager.minimumDetectionAngle = -50f;
        }
        else
        {
            _characterController.height = 1.7f;
            _playerAnimatorManager.NotSneaking();
            _isSneak = false;
            _playerMovement.CurrentSpeed = 5;
            CharacterManager.maximumDetectionAngle = 180f;
            CharacterManager.minimumDetectionAngle = -180f;
        }
    }

    private IEnumerator ExecuteAfterTime(float timeInSec, Action action)
    {
        yield return new WaitForSeconds(timeInSec);
        action?.Invoke();
    }
    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }
}

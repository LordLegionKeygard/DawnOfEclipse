using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private float _groundDistance = 1.2f;
    private Vector3 _velocity;
    private float _velocityUpdateY = -10;
    private float _jumpHeight = 1.5f;

    [Header("Bool")]
    private bool _inputAttackR1;
    private bool _inputAttackR2;
    public bool IsRoll;
    private bool _canNewAction = true;
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
    public float TimeR2;

    private void Awake()
    {
        _input = new PlayerInput();

        _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        _playerMovement = GetComponent<PlayerMovement>();
        _characterController = GetComponent<CharacterController>();
        _potionsControl = GetComponent<PotionsControl>();
        _staminaControl = GetComponent<StaminaControl>();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.TargetLock.performed += ctx => CustomEvents.FireCameraLockOnTarget();
        _input.Player.Sneak.performed += ctx => Sneaking();
        _input.Player.BlockL1.performed += ctx => Block(true);
        _input.Player.BlockL1.canceled += ctx => Block(false);
        _input.Player.Jump.performed += ctx => Jump();
        _input.Player.AttackR1.performed += ctx => _inputAttackR1 = true;
        _input.Player.AttackR1.canceled += ctx => _inputAttackR1 = false;
        _input.Player.AttackR2.performed += ctx => _inputAttackR2 = true;
        _input.Player.AttackR2.canceled += ctx => _inputAttackR2 = false;
        _input.Player.AttackR1.performed += ctx => CustomEvents.FirePoisonHandsParticle(true);
        _input.Player.AttackR2.performed += ctx => CustomEvents.FirePoisonHandsParticle(true);
        _input.Player.AttackR1.canceled += ctx => CustomEvents.FirePoisonHandsParticle(false);
        _input.Player.AttackR2.canceled += ctx => CustomEvents.FirePoisonHandsParticle(false);
        _input.Player.Run.performed += ctx => Run(true);
        _input.Player.Run.canceled += ctx => Run(false);
        _input.Player.Roll.canceled += ctx => Roll();
        _input.Player.PickUp.performed += ctx => CustomEvents.FirePickUp(true);
        _input.Player.PickUp.canceled += ctx => CustomEvents.FirePickUp(false);
        _input.Player.RaceSkill.performed += ctx => OnUseRaceSkill();
        _input.Player.RaceSkill.canceled += ctx => CustomEvents.OnPoisonHandsParticle(false);
    }

    private void Update()
    {
        if (HealthControl.IsDeath) return;

        if (_inputAttackR1) Attack(StaminaForR1, 1);
        if (_inputAttackR2) Attack(StaminaForR2, 2);

        if (_staminaControl.CurrentStamina <= 50)
        {
            { _playerAnimatorManager.PlayTargetBoolAnimation(false, _playerAnimatorManager.Block_L1, 0, 2); }
        }
        CheckGround();
    }

    private void OnUseRaceSkill()
    {
        if(!_canNewAction) return;
        CustomEvents.OnUseRaceSkill();
    }

    private void CheckGround()
    {
        _isGround = Physics.CheckSphere(groundCheck.position, _groundDistance, groundMask);
        if (_isGround && _velocity.y < 0) _velocity.y = _velocityUpdateY;
        {
            _velocity.y += _gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }
        if (!_isGround) { _playerAnimatorManager.PlayTargetBoolAnimation(true, _playerAnimatorManager.IsInAir, 0, 0); }
        if (_isGround) { _playerAnimatorManager.PlayTargetBoolAnimation(false, _playerAnimatorManager.IsInAir, 0, 0); }
    }

    private void Block(bool isPressed)
    {
        if (_staminaControl.CurrentStamina > 50 && isPressed && !IsRoll) { _playerAnimatorManager.PlayTargetBoolAnimation(true, _playerAnimatorManager.Block_L1, 0, 2); }
        else { _playerAnimatorManager.PlayTargetBoolAnimation(false, _playerAnimatorManager.Block_L1, 0, 2); }
    }

    private void Jump()
    {
        if (_isGround && !_isSneak && !IsRoll && !IsAttack && IsCanJump && _canNewAction && _staminaControl.CurrentStamina > 50)
        {
            _playerAnimatorManager.PlayTargetBoolAnimation(true, _playerAnimatorManager.Jump, 0, 0);
            _playerAnimatorManager.PlayTargetBoolAnimation(false, _playerAnimatorManager.Jump, 1, 0);
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            IsCanJump = false;
            _staminaControl.UseStamina(50);
        }
    }

    private void Attack(float stamina, int attackNumber)
    {
        if (_staminaControl.CurrentStamina > stamina && !IsAttack && _isGround && !IsRoll && !EventSystem.current.IsPointerOverGameObject())
        {
            switch (attackNumber)
            {
                case 1:
                    _playerAnimatorManager.PlayTargetBoolAnimation(true, _playerAnimatorManager.Attack_R1, 0, 1);
                    _playerAnimatorManager.PlayTargetBoolAnimation(false, _playerAnimatorManager.Attack_R1, TimeR1, 1);
                    break;
                case 2:
                    _playerAnimatorManager.PlayTargetBoolAnimation(true, _playerAnimatorManager.Attack_R2, 0, 1);
                    _playerAnimatorManager.PlayTargetBoolAnimation(false, _playerAnimatorManager.Attack_R2, TimeR2, 1);
                    break;
            }
        }
    }

    private void Run(bool isPressed)
    {
        bool _canNewMove = !IsAttack && _isGround && !_isSneak && !IsBlock;
        if (_staminaControl.CurrentStamina > 50 && _canNewMove && isPressed)
        {
            _playerAnimatorManager.PlayTargetBoolAnimation(true, _playerAnimatorManager.Run, 0, 4);
            if (!_potionsControl.SpeedPotion)
            {
                _staminaControl.StaminaRun = true;
                _playerMovement.CurrentSpeed = 7;
            }
            else { _potionsControl.PotionSpeed = 5; }
        }
        if (!isPressed && !_isSneak)
        {
            _playerAnimatorManager.PlayTargetBoolAnimation(false, _playerAnimatorManager.Run, 0.3f, 4);
            _staminaControl.StaminaRun = false;
            _playerMovement.CurrentSpeed = _playerMovement.DefaultSpeed;
            _potionsControl.PotionSpeed = 0;
        }
    }

    private void Roll()
    {
        bool _canNewMove = !IsAttack && _isGround && !IsBlock && _canNewAction;
        if (!IsFastRun && _staminaControl.CurrentStamina > 100 && _canNewMove)
        {
            _canNewAction = false;
            _playerAnimatorManager.PlayTargetBoolAnimation(true, _playerAnimatorManager.Roll, 0, 3);
            _playerAnimatorManager.PlayTargetBoolAnimation(false, _playerAnimatorManager.Roll, 0.5f, 3);
            Invoke("CanNewActionAfterRoll", 1f);
        }
    }

    private void CanNewActionAfterRoll()
    {
        _canNewAction = true;
    }

    private void Sneaking()
    {
        if (!_isGround) return;

        if (_isSneak == false)
        {
            Vector3 tempCenter = _characterController.center;
            tempCenter.y = _characterController.height / 2;
            _characterController.center = tempCenter;
            _playerAnimatorManager.PlayTargetBoolAnimation(true, _playerAnimatorManager.Sneak, 0, 0);
            _playerMovement.CurrentSpeed = 2;
            _isSneak = true;
            CharacterManager.maximumDetectionAngle = 50f;
            CharacterManager.minimumDetectionAngle = -50f;
        }
        else
        {
            _characterController.height = 1.7f;
            _playerAnimatorManager.PlayTargetBoolAnimation(false, _playerAnimatorManager.Sneak, 0, 0);
            _isSneak = false;
            _playerMovement.CurrentSpeed = 5;
            CharacterManager.maximumDetectionAngle = 180f;
            CharacterManager.minimumDetectionAngle = -180f;
        }
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.TargetLock.performed -= ctx => CustomEvents.FireCameraLockOnTarget();
        _input.Player.Sneak.performed -= ctx => Sneaking();
        _input.Player.BlockL1.performed -= ctx => Block(true);
        _input.Player.BlockL1.canceled -= ctx => Block(false);
        _input.Player.Jump.performed -= ctx => Jump();
        _input.Player.AttackR1.performed -= ctx => _inputAttackR1 = true;
        _input.Player.AttackR1.canceled -= ctx => _inputAttackR1 = false;
        _input.Player.AttackR2.performed -= ctx => _inputAttackR2 = true;
        _input.Player.AttackR2.canceled -= ctx => _inputAttackR2 = false;
        _input.Player.AttackR1.performed -= ctx => CustomEvents.FirePoisonHandsParticle(true);
        _input.Player.AttackR2.performed -= ctx => CustomEvents.FirePoisonHandsParticle(true);
        _input.Player.AttackR1.canceled -= ctx => CustomEvents.FirePoisonHandsParticle(false);
        _input.Player.AttackR2.canceled -= ctx => CustomEvents.FirePoisonHandsParticle(false);
        _input.Player.Run.performed -= ctx => Run(true);
        _input.Player.Run.canceled -= ctx => Run(false);
        _input.Player.Roll.canceled -= ctx => Roll();
        _input.Player.PickUp.performed -= ctx => CustomEvents.FirePickUp(true);
        _input.Player.PickUp.canceled -= ctx => CustomEvents.FirePickUp(false);
        _input.Player.RaceSkill.performed -= ctx => CustomEvents.OnUseRaceSkill();
        _input.Player.RaceSkill.canceled -= ctx => CustomEvents.OnPoisonHandsParticle(false);
    }
}

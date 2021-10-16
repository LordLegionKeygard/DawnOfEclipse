using System;
using System.Collections;
using UnityEngine;

public class PlayerController : CharacterManager
{
    public bool walk { get; set; }
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform cam;
    [SerializeField] private float speed;
    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float turnSmothTime;
    [SerializeField] private float turnSmoothVelocity;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float deceleration = 0.5f;
    private StaminaControl staminaControl;
    private Vector3 velocity;
    private float velocityMove;
    private Animator anim;
    public bool isGround;
    private bool roll;
    private bool sneak;
    private bool fastRun;
    public bool attack;
    public bool block;
    private static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int IsInAir = Animator.StringToHash("isInAir");
    private static readonly int Attack_R2_Trigger = Animator.StringToHash("AttackR2");
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Sneak = Animator.StringToHash("sneak");
    private static readonly int Jump_Trigger = Animator.StringToHash("jump");
    private static readonly int Roll = Animator.StringToHash("roll");
    private static readonly int Attack_R1_Trigger = Animator.StringToHash("AttackR1");
    private static readonly int Block_L1_Trigger = Animator.StringToHash("Block(L1)");
    private bool NotRoll => roll == false;
    private bool NotSneak => sneak == false;
    private bool IsAir => isGround == false;
    private bool NotAttack => attack == false;

    [Header("Time")]
    public float timeR1 = 1f;
    public float timeR1FastRun = 1f;
    public float timeR2 = 1f;
    public float timeL1 = 1.6f;

    private CameraLockOnTarget cameraLockOnTarget;

    private PlayerInput input;

    private HealthControl healthControl;

    Vector2 inputStrafe;

    private void Awake()
    {
        input = new PlayerInput();
        input.Player.TargetLock.performed += context => cameraLockOnTarget.TargetLock(); // wait new input system for last dualshock joysick
        input.Player.Sneak.performed += context => Sneaking();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        staminaControl = GetComponent<StaminaControl>();
        cameraLockOnTarget = GetComponent<CameraLockOnTarget>();
        healthControl = GetComponent<HealthControl>();

    }

    private void Update()
    {
        if (healthControl.currentHealth >= 1)
        {
            if (roll == false)
            {
                inputStrafe.x = Input.GetAxis("Horizontal");
                inputStrafe.y = Input.GetAxis("Vertical");

                anim.SetFloat("inputX", inputStrafe.x);
                anim.SetFloat("inputY", inputStrafe.y);
            }

            isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGround && velocity.y < 0) velocity.y = -2f;

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            anim.SetBool(IsInAir, IsAir);

            bool jumpDown = Input.GetKeyDown(KeyCode.JoystickButton0);

            bool canJump = jumpDown && isGround && NotSneak && NotRoll && NotAttack;

            if (canJump && staminaControl.CurrentStamina > 50 && !attack) // square
                Jump();

            if (walk)
                Walk();
        }

    }

    private void FixedUpdate()
    {
        bool canNewMove = !attack && isGround && walk && NotSneak && !block;

        bool canNewMoveR1 = !attack && isGround && NotSneak;

        if (Input.GetKey(KeyCode.JoystickButton7) && staminaControl.CurrentStamina > 250 && canNewMoveR1) // R2
        {
            staminaControl.UseStamina(250);
            AttackR2();
            StartCoroutine(ExecuteAfterTime(timeR2, DisableAttackR2));
        }

        if (Input.GetKey(KeyCode.JoystickButton5) && staminaControl.CurrentStamina > 100 && canNewMoveR1 && fastRun == false) // R1
        {
            staminaControl.UseStamina(100);
            AttackR1();
            StartCoroutine(ExecuteAfterTime(timeR1, DisableAttackR1));
        }
        if (Input.GetKey(KeyCode.JoystickButton5) && staminaControl.CurrentStamina > 150 && canNewMoveR1 && fastRun == true) // R1
        {
            staminaControl.UseStamina(150);
            AttackR1FastRun();
            StartCoroutine(ExecuteAfterTime(timeR1FastRun, DisableAttackR1));
        }

        if (Input.GetKey(KeyCode.JoystickButton4) && staminaControl.CurrentStamina > 100 && canNewMove) //L1
        {
            staminaControl.UseStamina(100);
            BlockL1();
            StartCoroutine(ExecuteAfterTime(timeL1, DisableBlockL1));
        }

        if (Input.GetKey(KeyCode.JoystickButton2) && staminaControl.CurrentStamina > 50 && canNewMove) // circle
        {
            staminaControl.staminaRun = true;
            speed = 7;
            anim.SetBool(Run, true);
            StartCoroutine(ExecuteAfterTime(0.3f, EnableFastRun));
        }

        else if (NotSneak)
        {
            staminaControl.staminaRun = false;
            speed = normalSpeed;
            anim.SetBool(Run, false);
            StartCoroutine(ExecuteAfterTime(0.3f, DisableFastRun));
        }
    }

    private void Jump()
    {
        staminaControl.UseStamina(50);
        anim.SetTrigger(Jump_Trigger);
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void Walk()
    {
        bool canNewMove = !attack && isGround && walk && NotSneak && !block;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.5f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            if (velocityMove < 1.0f)
            {
                velocityMove += Time.deltaTime * acceleration;
            }

            if (Input.GetKeyUp(KeyCode.JoystickButton2) && fastRun == false && staminaControl.CurrentStamina > 100 && canNewMove)
            {
                staminaControl.UseStamina(100);
                EnableRoll();
                StartCoroutine(ExecuteAfterTime(1.1f, DisableRoll));
            }
        }
        else
        {
            if (velocityMove > 0.0f)
            {
                velocityMove -= Time.deltaTime * deceleration;
            }
        }

        anim.SetFloat(Speed, velocityMove);
    }

    private void Sneaking()
    {
        if (isGround)
        {
            if (sneak == false)
            {
                Vector3 tempCenter = controller.center;
                tempCenter.y = controller.height / 2;
                controller.center = tempCenter;
                anim.SetBool(Sneak, true);
                speed = 2;
                sneak = true;
                CharacterManager.maximumDetectionAngle = 50f;
                CharacterManager.minimumDetectionAngle = -50f;
            }

            else
            {
                controller.height = 1.7f;
                anim.SetBool(Sneak, false);
                sneak = false;
                speed = 5;
                CharacterManager.maximumDetectionAngle = 180f;
                CharacterManager.minimumDetectionAngle = -180f;
            }
        }
    }

    private void EnableFastRun() =>
        fastRun = true;

    private void DisableFastRun() =>
        fastRun = false;

    private void AttackR2()
    {
        walk = false;
        attack = true;
        anim.SetBool(Attack_R2_Trigger, true);
    }

    private void AttackR1()
    {
        walk = false;
        attack = true;
        anim.SetBool(Attack_R1_Trigger, true);
    }
    private void AttackR1FastRun()
    {
        walk = false;
        attack = true;
        anim.SetBool(Attack_R1_Trigger, true);
    }

    private void BlockL1()
    {
        walk = false;
        block = true;
        anim.SetBool(Block_L1_Trigger, true);
    }

    private void DisableBlockL1()
    {
        walk = true;
        attack = false;
        block = false;
        anim.SetBool(Block_L1_Trigger, false);
    }
    private void DisableAttackR2()
    {
        walk = true;
        attack = false;
        anim.SetBool(Attack_R2_Trigger, false);
    }

    private void DisableAttackR1()
    {
        attack = false;
        anim.SetBool(Attack_R1_Trigger, false);
    }

    private void EnableRoll()
    {
        roll = true;
        anim.SetBool(Roll, true);
    }

    private void DisableRoll()
    {
        anim.SetBool(Roll, false);
        roll = false;
    }

    private IEnumerator ExecuteAfterTime(float timeInSec, Action action)
    {
        yield return new WaitForSeconds(timeInSec);
        action?.Invoke();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}

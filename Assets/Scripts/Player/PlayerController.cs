using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CharacterManager
{
    private Animator anim;
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
    private PlayerAnimatorManager playerAnimatorManager;
    private StaminaControl staminaControl;
    private PotionsControl potionsControl;
    private Vector3 velocity;
    public bool roll;
    private bool sneak;
    public bool fastRun;
    public bool attack;
    public bool block;
    private bool NotRoll => roll == false;
    private bool NotSneak => sneak == false;
    public bool isGround = false;
    private bool NotAttack => attack == false;

    [Header("Time")]
    public float timeR1 = 1f;
    public float timeR1FastRun = 1f;
    public float timeR2 = 1f;
    public float timeL1 = 1.6f;

    private CameraLockOnTarget cameraLockOnTarget;

    private PlayerInput input;

    private HealthControl healthControl;

    [SerializeField] private Inventory inventory;

    [SerializeField] private InventorySlot inventorySlot;

    [SerializeField] private Image[] iconImage;

    Vector2 inputStrafe;

    private bool canDrink = true;

    private void Awake()
    {
        input = new PlayerInput();
        input.Player.TargetLock.performed += context => cameraLockOnTarget.TargetLock(); // wait new input system for last dualshock joysick
        input.Player.Sneak.performed += context => Sneaking();
    }

    private void Start()
    {
        potionsControl = GetComponent<PotionsControl>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
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
            {
                velocity.y += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }
            if (isGround == false) { playerAnimatorManager.InAir(); }
            if (isGround) { playerAnimatorManager.OnGround(); }
            if (walk) Walk();
        }

    }

    private void CanDrink() { canDrink = true; }

    private void FixedUpdate()
    {
        bool canNewMove = !attack && isGround && walk && NotSneak && !block;
        bool canNewMoveR1 = !attack && isGround && NotSneak;
        bool canJump = isGround && NotSneak && NotRoll && NotAttack;

        if ((Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space)) && canJump && staminaControl.CurrentStamina > 50 && !attack) Jump(); //square

        if (Input.GetKey(KeyCode.Q))
        {
            for (int i = 0; i < iconImage.Length; i++)
            {
                if (iconImage[i].sprite.name == "Health_Potion_Icon" && canDrink == true)
                {
                    var potion = iconImage[i].GetComponentInParent<InventorySlot>();
                    potion.UseItem();
                    canDrink = false;
                    StartCoroutine(ExecuteAfterTime(2, CanDrink));
                }
            }
        }

        if (Input.GetKey(KeyCode.JoystickButton7) && staminaControl.CurrentStamina > 300 && canNewMoveR1) // R2
        {
            playerAnimatorManager.AttackR2();
            StartCoroutine(ExecuteAfterTime(timeR2, playerAnimatorManager.DisableAttackR2));
        }

        if (Input.GetKey(KeyCode.JoystickButton5) && staminaControl.CurrentStamina > 100 && canNewMoveR1 && fastRun == false) // R1
        {
            playerAnimatorManager.AttackR1();
            StartCoroutine(ExecuteAfterTime(timeR1, playerAnimatorManager.DisableAttackR1));
        }
        if (Input.GetKey(KeyCode.JoystickButton5) && staminaControl.CurrentStamina > 150 && canNewMoveR1 && fastRun == true) // R1
        {
            playerAnimatorManager.AttackR1FastRun();
            StartCoroutine(ExecuteAfterTime(timeR1, playerAnimatorManager.DisableAttackR1));
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton4) && staminaControl.CurrentStamina > 50) //L1 block true
        {
            playerAnimatorManager.BlockL1();

        }
        if (Input.GetKeyUp(KeyCode.JoystickButton4)) //L1 block false
        {
            playerAnimatorManager.DisableBlockL1();
        }

        if (staminaControl.CurrentStamina <= 50)
        {
            playerAnimatorManager.DisableBlockL1();
        }

        else if ((Input.GetKey(KeyCode.JoystickButton2) || Input.GetKey(KeyCode.LeftShift)) && staminaControl.CurrentStamina > 50 && canNewMove && potionsControl.speedPotion == false) // circle
        {
            staminaControl.staminaRun = true;
            speed = 7;
            playerAnimatorManager.Running();
            StartCoroutine(ExecuteAfterTime(0.3f, playerAnimatorManager.EnableFastRun));
        }
        else if ((Input.GetKey(KeyCode.JoystickButton2) || Input.GetKey(KeyCode.LeftShift)) && canNewMove && potionsControl.speedPotion == true) // circle
        {
            potionsControl.potionSpeed = 5;
            playerAnimatorManager.Running();
            StartCoroutine(ExecuteAfterTime(0.3f, playerAnimatorManager.EnableFastRun));
        }

        else if (NotSneak)
        {
            staminaControl.staminaRun = false;
            speed = normalSpeed;
            potionsControl.potionSpeed = 0;
            playerAnimatorManager.NotRunning();

            StartCoroutine(ExecuteAfterTime(0.3f, playerAnimatorManager.DisableFastRun));
        }
    }

    private void Jump()
    {
        playerAnimatorManager.JumpTrigger();
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
            controller.Move(moveDir.normalized * (speed + potionsControl.potionSpeed) * Time.deltaTime);

            if (playerAnimatorManager.velocityMove < 1.0f)
            {
                playerAnimatorManager.velocityMove += Time.deltaTime * acceleration;
            }

            if ((Input.GetKeyUp(KeyCode.JoystickButton2) || Input.GetKeyUp(KeyCode.LeftShift)) && fastRun == false && staminaControl.CurrentStamina > 100 && canNewMove)
            {
                playerAnimatorManager.EnableRoll();
                StartCoroutine(ExecuteAfterTime(1.1f, playerAnimatorManager.DisableRoll));
            }
        }
        else
        {
            if (playerAnimatorManager.velocityMove > 0.0f)
            {
                playerAnimatorManager.velocityMove -= Time.deltaTime * deceleration;
            }
        }

        playerAnimatorManager.Speeding();
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
                playerAnimatorManager.Sneaking();
                speed = 2;
                sneak = true;
                CharacterManager.maximumDetectionAngle = 50f;
                CharacterManager.minimumDetectionAngle = -50f;
            }

            else
            {
                controller.height = 1.7f;
                playerAnimatorManager.NotSneaking();
                sneak = false;
                speed = 5;
                CharacterManager.maximumDetectionAngle = 180f;
                CharacterManager.minimumDetectionAngle = -180f;
            }
        }
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

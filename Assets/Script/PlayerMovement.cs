using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerInputActions playerInputActions;
    private Animator animator;
    private Oxygen oxygen;

    private Player player;

    private Vector2 inputDirection;
    private Vector3 dashDirection;
    private bool isCrouching = false;
    private bool canDash = true;
    private bool isDashing = false;
    [SerializeField] private float dashCost = 10;
    private float dashTime;
    private Vector3 velocity; // 用于控制跳跃

    [SerializeField] private float crouchHeightMultiplier = 0.5f; // 调整 crouch 高度的倍率
    [SerializeField] private float speedReduction = 0.5f; // 速度减少的倍率
    private float defaultHeight;
    private Vector3 defaultCenter;
    private Vector3 originalScale; // 记录初始的缩放比例

    private Transform groundCheckPosition;

    public Vector3 checkpointPosition;
    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private Transform cameraTransfrom;
    [SerializeField] private Transform playerBody;

    private float xRotation = 0f; // 控制上下旋转
    private bool canLook = false;

    private void Awake()
    {
        oxygen = GetComponent<Oxygen>();
        playerInputActions = new PlayerInputActions();
        player = GetComponent<Player>();
        checkpointPosition = transform.position;
        cameraTransfrom = GetComponentInChildren<Camera>().transform;
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        originalScale = transform.localScale; // 保存原始比例
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Walk.performed += ctx => inputDirection = ctx.ReadValue<Vector2>();
        playerInputActions.Player.Walk.canceled += ctx => inputDirection = Vector2.zero;

        playerInputActions.Player.Crouch.performed += ctx => StartCrouching();
        playerInputActions.Player.Crouch.canceled += ctx => StopCrouching();

        playerInputActions.Player.Dash.performed += ctx => StartDashing();
        playerInputActions.Player.Jump.performed += ctx => Jump();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    private void Start()
    {
        groundCheckPosition = transform;
        SetDefaultCollisionBoxInfo();
        CameraInitialSetup();
    }

    private void Update()
    {
        if (!isDashing)
        {
            Move();
            ApplyGravity();
        }
        MouseLook();
        UpdateAnimatorParameters();
    }

    private void UpdateAnimatorParameters()
    {
        animator.SetFloat("xVelocity", inputDirection.x);
        animator.SetFloat("zVelocity", inputDirection.y);
    }

    private void MouseLook()
    {
        if (!canLook) return;

        float mouseX = playerInputActions.Player.Look.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float mouseY = playerInputActions.Player.Look.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -55f, 55f);

        cameraTransfrom.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void EnableLook() => canLook = true;

    private void Move()
    {
        if (inputDirection.magnitude >= 0.1f)
        {
            float currentSpeed = isCrouching ? player.crouchSpeed * speedReduction : player.walkSpeed;
            Vector3 moveDirection = transform.forward * inputDirection.y + transform.right * inputDirection.x;
            Vector3 moveVelocity = moveDirection * currentSpeed;

            characterController.Move(moveVelocity * Time.deltaTime);
        }
    }

    private void StartCrouching()
    {
        isCrouching = true;

        float crouchHeight = defaultHeight * crouchHeightMultiplier;
        Vector3 crouchCenter = defaultCenter * crouchHeightMultiplier;

        characterController.height = crouchHeight;
        characterController.center = defaultCenter;
        transform.localScale = new Vector3(originalScale.x, crouchHeightMultiplier, originalScale.z); // 修改缩放比例
        animator.SetBool("Crouch", true);
    }

    private void StopCrouching()
    {
        isCrouching = false;

        characterController.height = defaultHeight;
        characterController.center = defaultCenter;
        transform.localScale = originalScale; // 恢复到原始比例
        animator.SetBool("Crouch", false);
    }

    private void StartDashing()
    {
        if (canDash && inputDirection.magnitude > 0.1f)
        {
            isDashing = true;
            canDash = false;

            dashDirection = (transform.forward * inputDirection.y + transform.right * inputDirection.x).normalized;

            dashTime = player.dashDistance / player.dashSpeed;

            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        float timeElapsed = 0f;
        while (timeElapsed < dashTime)
        {
            Vector3 dashVelocity = dashDirection * player.dashSpeed;
            characterController.Move(dashVelocity * Time.deltaTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        StopDashing();
    }

    private void StopDashing()
    {
        isDashing = false;
        velocity.y = 0;
        oxygen.ConsumeOxygenForDash(dashCost);
        Invoke(nameof(ResetDash), player.dashCD);
    }

    private void ResetDash() => canDash = true;

    private void Jump()
    {
        if (characterController.isGrounded && !isDashing)
        {
            velocity.y = Mathf.Sqrt(player.jumpHeight * -2f * player.gravity);
            animator.SetBool("Jump", true);
        }
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            velocity.y += player.gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("Jump", false);
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    private void CameraInitialSetup()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        xRotation = 0f;
        cameraTransfrom.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        Invoke(nameof(EnableLook), 0.2f);
    }

    private void SetDefaultCollisionBoxInfo()
    {
        defaultHeight = characterController.height;
        defaultCenter = characterController.center;
    }
}

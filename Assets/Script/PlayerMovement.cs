using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector2 inputDirection; 
    private Vector3 dashDirection;

    private PlayerInputActions playerInputActions;
    private Player player;

    private bool isCrouching = false;
    private bool canDash = true;
    private bool isDashing = false;

    private float dashTime;
    private Vector3 velocity; //use to handle the vertical velocity(jump and gravity)

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckLength = 1.1f;
    [SerializeField] private LayerMask Layer;

    private Transform groundCheckPosition;  


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        player = GetComponent<Player>();
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
        characterController = GetComponent<CharacterController>();
        Layer = LayerMask.GetMask("Ground", "Obstacle");

        // set ground check position
        groundCheckPosition = transform;
    }

    private void Update()
    {
        if (!isDashing)
        {
            Move();
            ApplyGravity();
        }
        CheckGrounded();
    }

    private void Move()
    {
        if (inputDirection.magnitude >= 0.1f)
        {
            //get the current speed based on player status
            float currentSpeed = isCrouching ? player.crouchSpeed : player.walkSpeed;
            Vector3 moveDirection = transform.forward * inputDirection.y + transform.right * inputDirection.x;
            Vector3 moveVelocity = moveDirection * currentSpeed;

            characterController.Move(moveVelocity * Time.deltaTime);
        }
    }

    private void StartDashing()
    {
        if (canDash && inputDirection.magnitude > 0.1f )// dash only in the movement directio
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
        Invoke(nameof(ResetDash), player.dashCD);
    }

    private void ResetDash() => canDash = true;

    private void Jump()
    {
        if (isGrounded && !isDashing)
        {
            velocity.y = Mathf.Sqrt(player.jumpHeight * -2f * player.gravity);
        }
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            velocity.y += player.gravity * Time.deltaTime ;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    private void StartCrouching() => isCrouching = true;

    private void StopCrouching() => isCrouching = false;

    private void CheckGrounded()
    {
        RaycastHit hit;
        float rayLength = groundCheckLength + 0.1f;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;

        isGrounded = Physics.Raycast(rayOrigin, Vector3.down, out hit, rayLength, Layer);
    }
}

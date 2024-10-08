using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 inputDirection;
    private Vector3 dashDirection; 

    private PlayerInputActions playerInputActions;
    private Player player;

    private bool isCrouching = false;
    private bool canDash = true; 
    private bool isDashing = false;

    private float dashTime;

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
        rb = GetComponent<Rigidbody>();
    }

    private  void FixedUpdate()
    {
        if (!isDashing)
        {
            Move();
        }

    }

    private void Move()
    {
        if (inputDirection.magnitude >= 0.1f)
        {
            float currentSpeed = isCrouching ? player.crouchSpeed : player.walkSpeed;
            Vector3 moveDirection = transform.forward * inputDirection.y + transform.right * inputDirection.x;
            Vector3 moveVelocity = moveDirection * currentSpeed;

            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
        }
    }

    private void StartDashing()
    {
        if (canDash && inputDirection.magnitude > 0.1f)  // dash only in the movement direction
        {
            isDashing = true;
            canDash = false;

            dashDirection = (transform.forward * inputDirection.y + transform.right * inputDirection.x).normalized;

            dashTime = player.dashDistance / player.dashSpeed;
            rb.useGravity = false;

            StartCoroutine(PerformDash());
        }
    }

    // Coroutines that execute dash
    private IEnumerator PerformDash()
    {
        float timeElapsed = 0f;
        while (timeElapsed < dashTime)
        {
            // Update position at sprint speed every frame
            rb.velocity = dashDirection * player.dashSpeed;
            timeElapsed += Time.deltaTime;
            yield return null;  // wait for next frame
        }

        StopDashing();
    }

    private void StopDashing()
    {
        isDashing = false;
        rb.useGravity = true; 
        rb.velocity = Vector3.zero;  

        Invoke(nameof(ResetDash), player.dashCD);
    }

    private void ResetDash() => canDash = true;


    private void Jump()
    {
        if (!isDashing) 
        {
            rb.AddForce(Vector3.up * player.jumpForce, ForceMode.Impulse);
        }
    }


    private void StartCrouching() => isCrouching = true;
    


    private void StopCrouching() => isCrouching = false;

    
}

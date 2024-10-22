using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
<<<<<<< HEAD
<<<<<<< HEAD
    private Vector2 inputDirection; 
    private Vector3 dashDirection;

=======
>>>>>>> 3a03f53c2d2274ed749dafc316ef509e408a347b
=======
>>>>>>> eb0238db4ee4838792c557307acf49021f5f25ab
    private PlayerInputActions playerInputActions;
    private Oxygen oxygen;

    private Player player;

    private Vector2 inputDirection;
    private Vector3 dashDirection;
    private bool isCrouching = false;
    private bool canDash = true;
    private bool isDashing = false;
    [SerializeField] private float dashCost = 10;
    private float dashTime;
<<<<<<< HEAD
<<<<<<< HEAD
    private Vector3 velocity; //use to handle the vertical velocity(jump and gravity)
=======
    private Vector3 velocity;//Used to control jumping
>>>>>>> eb0238db4ee4838792c557307acf49021f5f25ab

    [SerializeField] private float crouchHeightMultiplier = 0.5f; 
    private float defaultHeight; 
    private Vector3 defaultCenter; 

<<<<<<< HEAD
    private Transform groundCheckPosition;  
=======
    private Vector3 velocity;//Used to control jumping

    [SerializeField] private float crouchHeightMultiplier = 0.5f; 
    private float defaultHeight; 
    private Vector3 defaultCenter; 

=======
>>>>>>> eb0238db4ee4838792c557307acf49021f5f25ab

    private Transform groundCheckPosition;

    public Vector3 checkpointPosition;
    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private Transform cameraTransfrom; 
    [SerializeField] private Transform playerBody; 

    private float xRotation = 0f; // Control up down rotation
    private bool canLook = false;
<<<<<<< HEAD
>>>>>>> 3a03f53c2d2274ed749dafc316ef509e408a347b
=======
>>>>>>> eb0238db4ee4838792c557307acf49021f5f25ab


    private void Awake()
    {
        oxygen = GetComponent<Oxygen>();
        playerInputActions = new PlayerInputActions();
        player = GetComponent<Player>();
        checkpointPosition = transform.position;
        cameraTransfrom = GetComponentInChildren<Camera>().transform;
        characterController = GetComponent<CharacterController>();
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
<<<<<<< HEAD
<<<<<<< HEAD
        characterController = GetComponent<CharacterController>();
        Layer = LayerMask.GetMask("Ground", "Obstacle");
=======
>>>>>>> eb0238db4ee4838792c557307acf49021f5f25ab

        groundCheckPosition = transform;
<<<<<<< HEAD
=======

        groundCheckPosition = transform;
        SetDefualtCllisionBoxInfo();
        CameraInitialSetup();
>>>>>>> 3a03f53c2d2274ed749dafc316ef509e408a347b
=======
        SetDefualtCllisionBoxInfo();
        CameraInitialSetup();
>>>>>>> eb0238db4ee4838792c557307acf49021f5f25ab
    }

    private void Update()
    {
        if (!isDashing)
        {
            Move();
            ApplyGravity();
        }
<<<<<<< HEAD
<<<<<<< HEAD
        CheckGrounded();
=======
        MouseLook(); 
>>>>>>> 3a03f53c2d2274ed749dafc316ef509e408a347b
=======
        MouseLook(); 
>>>>>>> eb0238db4ee4838792c557307acf49021f5f25ab
    }

    private void MouseLook()
    {
        if (!canLook) return;

        float mouseX = playerInputActions.Player.Look.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float mouseY = playerInputActions.Player.Look.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransfrom.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void EnableLook() => canLook = true;

    private void Move()
    {
        if (inputDirection.magnitude >= 0.1f)
        {
            float currentSpeed = isCrouching ? player.crouchSpeed : player.walkSpeed;
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
        characterController.center = crouchCenter;
    }

    private void StopCrouching()
    {
        isCrouching = false;

        characterController.height = defaultHeight;
        characterController.center = defaultCenter;
    }

    private void StartDashing()
    {
<<<<<<< HEAD
<<<<<<< HEAD
        if (canDash && inputDirection.magnitude > 0.1f)// dash only in the movement directio
=======
        if (canDash && inputDirection.magnitude > 0.1f)
>>>>>>> 3a03f53c2d2274ed749dafc316ef509e408a347b
=======
        if (canDash && inputDirection.magnitude > 0.1f)
>>>>>>> eb0238db4ee4838792c557307acf49021f5f25ab
        {
            isDashing = true;
            canDash = false;

            dashDirection = (transform.forward * inputDirection.y + transform.right * inputDirection.x).normalized;

            dashTime = player.dashDistance / player.dashSpeed;
<<<<<<< HEAD
=======

            
>>>>>>> 3a03f53c2d2274ed749dafc316ef509e408a347b

            

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
<<<<<<< HEAD
<<<<<<< HEAD
        velocity.y = 0;  
=======
        velocity.y = 0;
        oxygen.ConsumeOxygenForDash(dashCost);
>>>>>>> 3a03f53c2d2274ed749dafc316ef509e408a347b
=======
        velocity.y = 0;
        oxygen.ConsumeOxygenForDash(dashCost);
>>>>>>> eb0238db4ee4838792c557307acf49021f5f25ab
        Invoke(nameof(ResetDash), player.dashCD);
    }
    private void ResetDash() => canDash = true;

    private void Jump()
    {
<<<<<<< HEAD
<<<<<<< HEAD
        if (isGrounded && !isDashing)
=======
        if (characterController.isGrounded && !isDashing)
>>>>>>> 3a03f53c2d2274ed749dafc316ef509e408a347b
=======
        if (characterController.isGrounded && !isDashing)
>>>>>>> eb0238db4ee4838792c557307acf49021f5f25ab
        {
            velocity.y = Mathf.Sqrt(player.jumpHeight * -2f * player.gravity);
        }
    }

    private void ApplyGravity()
    {
<<<<<<< HEAD
<<<<<<< HEAD
        if (!isGrounded)
        {
            velocity.y += player.gravity * Time.deltaTime ;
=======
        if (!characterController.isGrounded)
        {
            velocity.y += player.gravity * Time.deltaTime;
>>>>>>> 3a03f53c2d2274ed749dafc316ef509e408a347b
=======
        if (!characterController.isGrounded)
        {
            velocity.y += player.gravity * Time.deltaTime;
>>>>>>> eb0238db4ee4838792c557307acf49021f5f25ab
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }
<<<<<<< HEAD

        characterController.Move(velocity * Time.deltaTime);
    }

<<<<<<< HEAD
    private void StartCrouching() => isCrouching = true;
=======

        characterController.Move(velocity * Time.deltaTime);
    }

 
    private void CameraInitialSetup()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the mouse pointer
        Cursor.visible = false;
>>>>>>> 3a03f53c2d2274ed749dafc316ef509e408a347b

        xRotation = 0f;
        cameraTransfrom.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

<<<<<<< HEAD
    private void CheckGrounded()
=======
 
    private void CameraInitialSetup()
>>>>>>> eb0238db4ee4838792c557307acf49021f5f25ab
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the mouse pointer
        Cursor.visible = false;

<<<<<<< HEAD
        isGrounded = Physics.Raycast(rayOrigin, Vector3.down, out hit, rayLength, Layer);
=======
=======
        xRotation = 0f;
        cameraTransfrom.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

>>>>>>> eb0238db4ee4838792c557307acf49021f5f25ab
        Invoke(nameof(EnableLook), 0.2f);
    }
    private void SetDefualtCllisionBoxInfo()
    {
        defaultHeight = characterController.height;
        defaultCenter = characterController.center;
<<<<<<< HEAD
>>>>>>> 3a03f53c2d2274ed749dafc316ef509e408a347b
=======
>>>>>>> eb0238db4ee4838792c557307acf49021f5f25ab
    }
}

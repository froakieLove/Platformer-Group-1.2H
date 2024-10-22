using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
<<<<<<< HEAD
    [SerializeField] public float walkSpeed = 2f; 
    [SerializeField] public float crouchSpeed = 1; 

    [Header("Jump Settings")]
    [SerializeField] public float jumpHeight = 2f;

    [Header("Dash Settings")]
    [SerializeField] public float dashDistance = 5f; 
    [SerializeField] public float dashCD = 1;       
    [SerializeField] public float dashSpeed = 10f;   

    [Header("Gravity Settings")]
    [SerializeField] public float gravity = -9.81f;  

=======
    [SerializeField] public float walkSpeed = 2f;
    [SerializeField] public float crouchSpeed = 1f;

    [Header("Jump Settings")]
    [SerializeField] public float jumpHeight = 1f;

    [Header("Dash Settings")]
    [SerializeField] public float dashDistance = 5f;
    [SerializeField] public float dashCD = 1f;
    [SerializeField] public float dashSpeed = 10f;
>>>>>>> 3a03f53c2d2274ed749dafc316ef509e408a347b

    [Header("Gravity Settings")]
    [SerializeField] public float gravity = -9.81f;

    [SerializeField] public Vector3 checkpointPosition;
    [SerializeField] private float deathnHight = 45;

    public float dashOxygenCost { get; internal set; }

    private CharacterController characterController;
    private bool isFrozen = false; 

    private void Start()
    {
        checkpointPosition = transform.position;
        characterController = GetComponent<CharacterController>();

        if (characterController == null)
        {
            Debug.LogError("CharacterController component is required on the player.");
        }
    }

    private void Update()
    {
        //die when fall off the map
        if (transform.position.y < deathnHight)
        {
            Death();
        }

        //Reload the current scene£¨will delete later£©
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Death()
    {
        Debug.Log("Player has died.");

        transform.position = checkpointPosition;

        

        FreezePlayer();

        // Start a coroutine to delay 0.5 seconds to unfreeze the player and reset oxygen
        StartCoroutine(UnfreezeAfterDelay(0.5f));
    }

    private void FreezePlayer()
    {
        if (characterController != null)
        {
            characterController.enabled = false; // Disable the CharacterController to prevent movement
            isFrozen = true;
        }
    }

    private void UnfreezePlayer()
    {
        if (characterController != null)
        {
            characterController.enabled = true; // Enable CharacterController to resume movement
            isFrozen = false;
        }
    }

    private IEnumerator UnfreezeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        UnfreezePlayer();

        ResetOxygen();
    }

    private void ResetOxygen()
    {
        Oxygen oxygenBar = GetComponentInChildren<Oxygen>();
        if (oxygenBar != null)
        {
            oxygenBar.ResetOxygen();
        }
        else
        {
            Debug.LogError("Oxygen component not found on player.");
        }
    }
}

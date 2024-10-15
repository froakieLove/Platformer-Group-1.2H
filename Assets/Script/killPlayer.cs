using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Attach script to gameObject that will kill the player
    // Make sure gameObject has a collider component and isTrigger true

public class KillPlayer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        CharacterController characterController = other.GetComponent<CharacterController>();
        characterController.enabled = false;
        other.GetComponent<Transform>().position = other.GetComponent<PlayerMovement>().checkpointPosition;
        characterController.enabled = true;
    }
}


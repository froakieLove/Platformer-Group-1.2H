using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Place gameObject where the player will respawn
// Attach script to gameObject with a collider component and isTrigger true

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerMovement>().checkpointPosition = transform.position;
    }
}

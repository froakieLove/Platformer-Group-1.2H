using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Place gameObject where the player will respawn
// Attach script to gaemObject with a collider component and isTrigger true

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.checkpointPosition = transform.position;
                Debug.Log("Checkpoint position set to: " + transform.position);
            }
            else
            {
                Debug.LogError("Player component not found on the object that triggered the checkpoint.");
            }
        }
    }
}
 
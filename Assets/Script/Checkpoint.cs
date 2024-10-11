using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Place gameObject where the player will respawn
// Attach script to gaemObject with a collider component and isTrigger true

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        other.GetComponent<Player>().checkpointPosition = transform.position;
    }
}

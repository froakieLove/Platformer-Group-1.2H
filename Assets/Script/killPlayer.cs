using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Attach script to gameObject that will kill the player
    // Make sure gameObject has a collider component and isTrigger true

public class killPlayer : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        other.GetComponent<Transform>().position = other.GetComponent<Player>().checkpointPosition;
    }
}

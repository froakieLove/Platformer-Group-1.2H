using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{

    public Platform platform;

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            
            platform.Activate();
        }
    }
}
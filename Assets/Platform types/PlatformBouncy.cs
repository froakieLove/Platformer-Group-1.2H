using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBouncy : MonoBehaviour
{
    [SerializeField] private float jumpMultiplier = 3;

    void OnTriggerEnter(Collider other){

        if (other.CompareTag("Player")){
            Player player = other.GetComponent<Player>();
            player.jumpHeight *= jumpMultiplier;
        }
    }

    void OnTriggerExit(Collider other){
        if (other.CompareTag("Player")){
            Player player = other.GetComponent<Player>();
            player.jumpHeight /= jumpMultiplier;

        }
    }
}

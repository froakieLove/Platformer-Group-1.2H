using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float walkSpeed = 2f;   
    [SerializeField] public float crouchSpeed = 1f;
    [SerializeField] public float jumpForce = 5f; 

    [SerializeField] public float dashDistance = 5f;
    [SerializeField] public float dashCD = 1f;
    [SerializeField] public float dashSpeed = 10f;

    [SerializeField] public Vector3 checkpointPosition;

    void Start(){
        checkpointPosition = transform.position;
    }
}


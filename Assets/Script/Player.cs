using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
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


    [SerializeField] public Vector3 checkpointPosition;

    public float dashOxygenCost { get; internal set; }


    void Start()
    {
        checkpointPosition = transform.position;
    }

    public void Death()
    {
        Debug.Log("Player has died.");

        Respawn();
    }
    public void Respawn()
    {
        Debug.Log("Respawning Player and reset OxygenBar");
        Invoke(nameof(ResetOxygenAfterDelay), 1f);
    }
    private void ResetOxygenAfterDelay()
    {
        OxygenBar oxygenBar = GetComponentInChildren<OxygenBar>();
        if (oxygenBar != null)
        {
            oxygenBar.ResetOxygen();
        }
    }
}


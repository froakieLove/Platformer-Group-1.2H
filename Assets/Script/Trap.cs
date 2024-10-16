using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float oxygenCost = 10f;
    public float reduceFrequency = 1f;

    private float nextReduceTime = 0;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= nextReduceTime)
        {
            Oxygen oxygenBar = other.GetComponentInChildren<Oxygen>();
            if (oxygenBar != null)
            {
                oxygenBar.ReduceOxygen(oxygenCost);
                nextReduceTime = Time.time + reduceFrequency;  // update the time of cost oxygen
            }
        }
    }
}
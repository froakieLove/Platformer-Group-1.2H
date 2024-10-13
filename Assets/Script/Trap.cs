using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float oxygenCost = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OxygenBar oxygenBar = other.GetComponentInChildren<OxygenBar>();
            if (oxygenBar != null)
            {
                oxygenBar.ReduceOxygen(oxygenCost);
            }
        }
    }
}

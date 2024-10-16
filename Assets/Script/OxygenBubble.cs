using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenBubble : MonoBehaviour
{
    public float oxygenAmount = 10f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Oxygen oxygenBar = other.GetComponentInChildren<Oxygen>();
            if (oxygenBar != null)
            {
                oxygenBar.AddOxygen(oxygenAmount); 
            }

            gameObject.SetActive(false);
        }

      
    }
}
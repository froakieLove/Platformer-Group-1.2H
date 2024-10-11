using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenBubble : MonoBehaviour
{
    public float oxygenAmount = 10f; // 添加到氧气条的氧气量

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //test for is player has collided with the bubble
            Debug.Log("Player has collided with the bubble");

            OxygenBar oxygenBar = other.gameObject.GetComponent<OxygenBar>();
            if (oxygenBar != null)
            {
                oxygenBar.AddOxygen(oxygenAmount);
                Destroy(gameObject); // 捡起后销毁氧气泡泡
            }

        }
    }
}
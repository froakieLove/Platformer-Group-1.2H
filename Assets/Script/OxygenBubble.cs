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
            OxygenBar oxygenBar = other.GetComponentInChildren<OxygenBar>();
            if (oxygenBar != null)
            {
                oxygenBar.AddOxygen(oxygenAmount); // 调用增加氧气的函数
            }

            gameObject.SetActive(false);
        }

      
    }
}
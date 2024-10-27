using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public float disappearDelay = 0.5f;

    public void Activate(){
        Invoke("Deactivate", disappearDelay);
    }

    void Deactivate(){
        gameObject.SetActive(false); 
    }
}

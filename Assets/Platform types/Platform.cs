using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public enum PlatformType
    {
        Disappearing, // Sink very quickly
        Sinking, // Sink slowly
        Floating, // Sink upwards
        Bouncy,
        Moving,
    }

    [SerializeField] private PlatformType platformType; // Set this in the editor
    [SerializeField] private float activationDelay = 0.5f;
    private Vector3 initialPosition;
    private bool resetting = false;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float moveDistance = 5f;
    private bool sinking = false;


    void Start(){
        initialPosition = transform.parent.position;
    }

    public void Activate()
    {

        switch(platformType)
        {
            case PlatformType.Disappearing:
                moveSpeed = 4f;
                Invoke("Sink", activationDelay);
                break;

            case PlatformType.Sinking:
                moveSpeed = 1f;
                Invoke("Sink", activationDelay);
                break;

            case PlatformType.Floating:
                moveSpeed = -1f;
                Invoke("Sink", activationDelay);
                break;

            case PlatformType.Bouncy:
                break;

            case PlatformType.Moving:
                break;

        }
    }

    private void Update()
    {
        if (sinking)
        {
            Sink();
        }
        else if (resetting){
            ResetPlatform();
        }
    }

    void ResetPlatform(){
        resetting = true;
        transform.parent.position += Vector3.down * -moveSpeed * Time.deltaTime;
        
        if (Vector3.Distance(transform.parent.position, initialPosition) < 0.1f)
        {
            resetting = false;
        }
    }

    void Sink(){
        sinking = true;
        transform.parent.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.parent.position, initialPosition) > moveDistance)
        {
            sinking = false;
            Invoke("ResetPlatform", activationDelay);
        }
    }
}

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
    }

    public PlatformType platformType; // Set this in the editor
    private Vector3 initialPosition;
    public float activationDelay = 0.5f;
    public float sinkSpeed;
    private bool sinking = false;

    void Start(){
        initialPosition = transform.parent.position;
    }

    public void Activate()
    {

        switch(platformType)
        {
            case PlatformType.Disappearing:
                sinkSpeed = 4f;
                Invoke("Sink", activationDelay);
                break;

            case PlatformType.Sinking:
                sinkSpeed = 1f;
                Invoke("Sink", activationDelay);
                break;

            case PlatformType.Floating:
                sinkSpeed = -1f;
                Invoke("Sink", activationDelay);
                break;

        }
    }

    private void Update()
    {
        if (sinking)
        {
            Sink();
        }
    }

    void ResetPlatform(){
            transform.parent.position = initialPosition;
    }

    void Sink(){
        sinking = true;
        transform.parent.position += Vector3.down * sinkSpeed * Time.deltaTime;

        if (transform.parent.position.y < initialPosition.y - 10f || transform.parent.position.y > initialPosition.y + 10f)
        {
            sinking = false;
            ResetPlatform();
        }
    }
}

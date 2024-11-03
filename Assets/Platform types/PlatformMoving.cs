using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    private Vector3 initialPosition;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float moveDistance = 5f;

    void Start(){
        initialPosition = transform.parent.position;
    }

    private void Update()
    {
        // blue arrow in editor
        transform.parent.position += Vector3.forward * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.parent.position, initialPosition) > moveDistance)
        {
            moveSpeed = -moveSpeed;
        }
    }
}

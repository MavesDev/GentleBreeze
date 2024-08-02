using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    void Update()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //Debug.Log("Player Position: " + player.position);
        //Debug.Log("Desired Position: " + desiredPosition);
        //Debug.Log("Smoothed Position: " + smoothedPosition);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 15f;


    // Update is called once per frame
    void Update()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        float newSize = Camera.main.orthographicSize - scrollData * zoomSpeed;

        newSize = Mathf.Clamp(newSize, minZoom, maxZoom);

        Camera.main.orthographicSize = newSize;
    }
}

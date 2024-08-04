using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public float sensitivity = 5f;
    private Camera cam;

    private Vector3 mousePosition;
    private Vector2 offset;
    private Vector2 screenPoint;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
    }

    void RotatePlayer()
    {
        mousePosition = Input.mousePosition;
        screenPoint = cam.WorldToScreenPoint(transform.position);
        offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, sensitivity * Time.deltaTime);
    }
}

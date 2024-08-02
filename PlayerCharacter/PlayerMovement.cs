using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    //private Vector2 moveInput;
    private Vector2 moveVelocity;

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(xInput) > 0)
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }

        if (Mathf.Abs(yInput) > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, yInput * moveSpeed);
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        //Vector2 direction = new Vector2(xInput, yInput).normalized;
        //body.velocity = direction * speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public bool isDash = false;
    private float dashTime;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDash)
        {
            if (Random.Range(0, 100) < 1)
            {
                StartDash();
            }
        }

        if (isDash)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                EndDash();
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void StartDash()
    {
        isDash = true;
        dashTime = dashDuration;
        moveSpeed = dashSpeed;
    }

    void EndDash()
    {
        isDash = false;
        moveSpeed = 3f;
    }

}

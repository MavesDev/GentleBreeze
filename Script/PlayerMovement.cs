using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    [Header("Walk & Run Setting")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    private float moveSpeed;

    [Header("Dash Setting")]
    [SerializeField] float dashSpeed = 50f;
    [SerializeField]  float dashDuration = 0.2f;
    [SerializeField]  float dashCooldown = 1f;
    bool isDashing;
    bool canDash;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        moveVelocity.x = Input.GetAxis("Horizontal");
        moveVelocity.y = Input.GetAxis("Vertical");

        if(moveVelocity.sqrMagnitude > 1)
        {
            moveVelocity.Normalize();
        }

        rb.velocity = new Vector2(moveVelocity.x * moveSpeed, moveVelocity.y * moveSpeed);
        

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }

        
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(moveVelocity.x * dashSpeed, moveVelocity.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}

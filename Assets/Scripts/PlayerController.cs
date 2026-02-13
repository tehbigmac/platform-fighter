using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed;
    private float jumpForce = 10;

    private bool onGround;
    private float jumps = 0;
    private float dodgeCooldown = 0;

    private bool invulnerable = false;

    private float stickNull = 6741; //arbitrary value can be anything over 360

    private Vector2 moveValue;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onGround) {
            moveSpeed = 8;
        } else {
            moveSpeed = 10;
        }

        rb.linearVelocity = new Vector3(moveValue.x * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

        if (moveValue.y <= -0.5 && !onGround) {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -20, rb.linearVelocity.z);
        }
    }

    // INPUT FUNCTIONS ------------------------------------------------------------------------------------------
    private void OnJump(InputValue value)
    {
        if (jumps > 0) {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            jumps --;
            onGround = false;
        }
    }

    private void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    private void OnDodge(InputValue value) 
    {
        float angle = CheckStickAngle();
        if (dodgeCooldown <= 0) {
            if (angle > 315 && angle <= 360 || angle < 45 && angle >= 0 ) {
                Debug.Log("dodge right");
            }
            if (angle > 45 && angle <= 135) {
                Debug.Log("dodge up");
            }
            if (angle > 135 && angle <= 225) {
                Debug.Log("dodge left");
            }
            if (angle > 225 && angle <= 315) {
                Debug.Log("dodge down");
            }
        }
    }
    

    // DETECTOR FUNCTIONS ------------------------------------------------------------------------------------------
    private void OnCollisionEnter(Collision collision) {
        onGround = true;
        jumps = 2;
    }

    public float CheckStickAngle() {
        //doom blackout coding idek but it works ig
        if (moveValue.x > 0 && moveValue.y != 0) 
        {
            invulnerable = true;
            float stickAngle = Mathf.Rad2Deg * Mathf.Atan(moveValue.y / moveValue.x);
            stickAngle = (stickAngle + 360) % 360;

            Debug.Log("angle of stick 1 is " + stickAngle);
            return stickAngle;
        }
        else if (moveValue.x < 0 && moveValue.y != 0)
        {
            invulnerable = true;
            float stickAngle = Mathf.Rad2Deg * Mathf.Atan(moveValue.y / moveValue.x);
            stickAngle = (stickAngle + 180) % 360;

            Debug.Log("angle of stick 2 is " + stickAngle);
            return stickAngle;
        } 
        else {
            Debug.Log("hi :D");
            return stickNull;
        }
    }
}

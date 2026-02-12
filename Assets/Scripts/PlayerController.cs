using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed;
    private float jumpForce = 10;

    private bool onGround;
    private float jumps = 0;

    private bool invulnerable = false;

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

    private void OnDodge(InputValue value) {
        if (moveValue.x >= 0 || moveValue.y >= 0) {
            invulnerable = true;
            float stickAngle = Mathf.Atan(moveValue.y / moveValue.x);
            Debug.Log("angle of stick is " + stickAngle);
            if (stickAngle > -0.45  && stickAngle > 0.45) {
                Debug.Log("dodged right");
            } else if (stickAngle > 0.45  && stickAngle < 1.45) {
                Debug.Log("dodged up");
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        onGround = true;
        jumps = 2;
    }
}

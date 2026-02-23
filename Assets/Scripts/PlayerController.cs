using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed;
    private float jumpForce = 10;

    private bool onGround;
    private float jumps = 0;
    private float fastFallSpeed = -20;
    private float dodgeCooldown = 0;
    private float dodgeForce = 20;

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

        EditXV(moveValue.x * moveSpeed);

        if (moveValue.y <= -0.5 && !onGround) {
            AddLinearVelocity(0, fastFallSpeed, 0);
        }
    }

    // INPUT FUNCTIONS ------------------------------------------------------------------------------------------
    private void OnJump(InputValue value)
    {
        if (jumps > 0) {
            EditYV(jumpForce);
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
                EditXV(dodgeForce);
                Debug.Log("dodge right");
            }
            if (angle > 45 && angle <= 135) {
                EditYV(dodgeForce);
                Debug.Log("dodge up");
            }
            if (angle > 135 && angle <= 225) {
                EditXV(-dodgeForce);
                Debug.Log("dodge left");
            }
            if (angle > 225 && angle <= 315) {
                EditYV(-dodgeForce);
                Debug.Log("dodge down");
            }
        }
    }
    

    // DETECTOR FUNCTIONS ------------------------------------------------------------------------------------------
    private void OnCollisionEnter(Collision collision) {
        onGround = true;
        jumps = 2;
    }

    // MATHY FUNCTIONS ------------------------------------------------------------------------------------------
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
        } else if ((moveValue.x == 0 && moveValue.y != 0) || (moveValue.x != 0 && moveValue.y == 0)) {
            if (moveValue.x > 0) 
            {
                return 0;
            } 
            else if (moveValue.x < 0) 
            {
                return  180;
            } 
            else if (moveValue.y > 0) 
            {
                return 90;
            } 
            else (moveValue.y < 0) 
            {
                return  270;
            }
            Debug.Log ("wasd input detected");
        }
        else {
            Debug.Log("hi :D");
            return stickNull;
        }
    }

    public void AddLinearVelocity(float x, float y, float z) {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x + x, rb.linearVelocity.y + y, rb.linearVelocity.z + z);
    }

    public void MultLinearVelocity(float x, float y, float z) {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * x, rb.linearVelocity.y * y, rb.linearVelocity.z * z);
    }

    public void EditXV(float x) {
        rb.linearVelocity = new Vector3(x, rb.linearVelocity.y, rb.linearVelocity.z);
    }

    public void EditYV(float y) {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, y, rb.linearVelocity.z);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UIElements.Experimental;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed;
    
    private float jumpForce = 14;
    private bool onGround;
    private float jumpValue; // returns 1 if player is jumping, 0 if not
    private bool jumping; // returns true if the player is in the jumping state, false if it is not. technically redundant but booleans are so much easier to read
    private float jumps = 0; // how many jumps the player has left

    private float fastFallSpeed = -10;
    private float dodgeCooldown = 0;
    private float dodgeForce = 10;

    private bool invulnerable = false;

    private float stickNull = 6741; //arbitrary value can be anything over 360

    private Vector2 moveValue;

    // UNITY STUFF ------------------------------------------------------------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //GROUND VS AIR SPEED
        if (!onGround) {
            moveSpeed = 8;
        } else {
            moveSpeed = 15;
        }

        //MOVEMENT UPDATES
        if (!invulnerable || onGround) {
            EditXV(((moveValue.x / 2) + (Math.Sign(moveValue.x) * 0.5f)) * moveSpeed); // modified horizontal velocity (now range 0.5 - 1)
        }

        //FASTFALL
        if (moveValue.y <= -0.5 && !onGround) {
            AddLinearVelocity(0, fastFallSpeed, 0);
        }
        
        if (jumping) {
            EditYV(jumpForce);
        }
    }


    // COROUTINES ------------------------------------------------------------------------------------------
    IEnumerator IFrames() {
        yield return new WaitForSeconds(0.67f);
        invulnerable = false;
    }

    // IEnumerator JumpTime() {
    //     yield return idk bro go find what to put here :D
    // }

    // INPUT FUNCTIONS ------------------------------------------------------------------------------------------
    public void Jump(InputValue value)
    {
        if (jumps > 0) {
            jumpValue = value.Get<float>();
            if (jumpValue == 1) {
                jumping = true;
            } else {
                jumping = false;
            }
            //Debug.Log("jump button value is uh " + jumpValue + " i think");
        }    
    }

    public void Release()
    {
        EditYV(rb.linearVelocity.y * 0.5f);
    }

    public void Move(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    public void Dodge()
    {
        float angle = CheckStickAngle();
        if (invulnerable == false) {
            invulnerable = true;
            StartCoroutine(IFrames());
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
            float stickAngle = Mathf.Rad2Deg * Mathf.Atan(moveValue.y / moveValue.x);
            stickAngle = (stickAngle + 360) % 360;

            return stickAngle;
        }
        else if (moveValue.x < 0 && moveValue.y != 0)
        {
            float stickAngle = Mathf.Rad2Deg * Mathf.Atan(moveValue.y / moveValue.x);
            stickAngle = (stickAngle + 180) % 360;

            return stickAngle;
        }
        //  else if ((moveValue.x == 0 && moveValue.y != 0) || (moveValue.x != 0 && moveValue.y == 0)) 
        // {
        //     if (moveValue.x > 0) 
        //     {
        //         return 0;
        //     } 
        //     else if (moveValue.x < 0) 
        //     {
        //         return  180;
        //     } 
        //     else if (moveValue.y > 0) 
        //     {
        //         return 90;
        //     } 
        //     else (moveValue.y < 0) 
        //     {
        //         return  270;
        //     }
        //     Debug.Log ("wasd input detected");
        // }
        else 
        {
            Debug.Log("hi :D");
            return stickNull;
        }
    }

    // VELOCITY FUNCTIONS ------------------------------------------------------------------------------------------

    public void AddLinearVelocity(float x, float y, float z) {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x + x, rb.linearVelocity.y + y, rb.linearVelocity.z + z);
    }

    public void MultLinearVelocity(float x, float y, float z) {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * x, rb.linearVelocity.y * y, rb.linearVelocity.z * z);
    }

    public void EditXV(float x) {
        rb.linearVelocity = new Vector3(x, rb.linearVelocity.y, rb.linearVelocity.z);
        // Debug.Log("linear x velocity set to " + x);
    }

    public void EditYV(float y) {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, y, rb.linearVelocity.z);
        // Debug.Log("linear y velocity set to " + y);
    }
}

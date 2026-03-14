using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UIElements.Experimental;
using System;

public class PlayerController : MonoBehaviour
{
    public GameObject NAtk;

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

    private bool cantMove = false;

    private float stickNull = 6741; //arbitrary value can be anything over 360

    private Vector2 moveValue;

    private Vector2 kbVel;
    private float kbVelDecay = 1;


    public float KB;             // JAKE USE THESE VARIABLES THANK YOU
    public int lives;


    // UNITY STUFF ------------------------------------------------------------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameManager gm = FindFirstObjectByType<GameManager>();
        gm.AddPlayer(this);
        lives = 3;

        NAtk.SetActive(false);

    }

    void FixedUpdate()
    {
        //GROUND VS AIR SPEED
        if (!onGround) {
            moveSpeed = 8;
        } else {
            moveSpeed = 15;
        }

        //MOVEMENT UPDATES
        if (!cantMove || onGround)
        {
            EditXV(((moveValue.x / 2) + (Math.Sign(moveValue.x) * 0.5f)) * moveSpeed); // modified horizontal velocity (now range 0.5 - 1)
        }
        else 
        {
            EditXV(0);
        }

        if (Mathf.Abs(kbVel.x) > kbVelDecay)
        {
            kbVel.x -= kbVelDecay * Mathf.Sign(kbVel.x);
        }
        else {
            kbVel.x = 0;
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
        cantMove = false;
    }

    IEnumerator WaitToDelete(float s, GameObject gb) {
        yield return new WaitForSeconds(s);
        gb.SetActive(false);
    }

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
        EditYV(rb.linearVelocity.y * 2);
    }

    public void Attack(InputValue value) 
    {
        if (!cantMove)
        {
            float angle = SimplifyStickAngle();

            if (angle == stickNull)
            {
                NAtk.SetActive(true);
                StartCoroutine(WaitToDelete(0.5f, NAtk));
            }

            Debug.Log("Attack in direction " + angle);
        }
    }

    public void StrongAttack(InputValue value) 
    {
        float angle = SimplifyStickAngle();

        Debug.Log("Strong Attack in direction " + angle);
    }
    
    public void Special(InputValue value)
    {
        float angle = SimplifyStickAngle();

        Debug.Log("Special in direction " + angle);
    }


    public void Move(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    public void Dodge()
    {
        float angle = CheckStickAngle();
        if (cantMove == false) {
            cantMove = true;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack")) 
        {
            //Debug.Log("ow");
            var atkStats = other.GetComponent<attack>();
            ReceiveAttack(atkStats.GetDamage(), atkStats.GetKb(), atkStats.GetAngle());
        }
        if (other.CompareTag("Blast Zone")) 
        {
            Die();
        }
    }

    // MISC ------------------------------------------------------------------------------------------
    public void Die() {
        transform.position = new Vector3(0, 6, 0);
        rb.linearVelocity = new Vector3(0, 0, 0);
        lives --;
    }

    // MATHY FUNCTIONS ------------------------------------------------------------------------------------------

    private void ReceiveAttack(float dmg, float kb, float angle)
    {
        cantMove = true;
        angle *= Mathf.Deg2Rad;
        KB += 0.1f * dmg;

        kbVel.y = (1 + (KB * kb) * Mathf.Sin(angle));
        kbVel.x = (1 + (KB * kb) * Mathf.Cos(angle));

        Debug.Log("attack recieved: dmg = " + dmg + " kb = " + kb + " angle = " + angle);
    }
    public float SimplifyStickAngle() {
        if (moveValue.x > 0 && moveValue.y != 0)
        {
            float stickAngle = Mathf.Rad2Deg * Mathf.Atan(moveValue.y / moveValue.x);
            stickAngle = (stickAngle + 360) % 360;

            if (stickAngle > 315 && stickAngle <= 360 || stickAngle < 45 && stickAngle >= 0)
            {
                stickAngle = 0;
            }
            if (stickAngle > 45 && stickAngle <= 135)
            {
                stickAngle = 90;
            }
            if (stickAngle > 135 && stickAngle <= 225)
            {
                stickAngle = 180;
            }
            if (stickAngle > 225 && stickAngle <= 315)
            {
                stickAngle = 270;
            }

            return stickAngle;
        }
        else if (moveValue.x < 0 && moveValue.y != 0)
        {
            float stickAngle = Mathf.Rad2Deg * Mathf.Atan(moveValue.y / moveValue.x);
            stickAngle = (stickAngle + 180) % 360;

            if (stickAngle > 315 && stickAngle <= 360 || stickAngle < 45 && stickAngle >= 0)
            {
                stickAngle = 0;
            }
            if (stickAngle > 45 && stickAngle <= 135)
            {
                stickAngle = 90;
            }
            if (stickAngle > 135 && stickAngle <= 225)
            {
                stickAngle = 180;
            }
            if (stickAngle > 225 && stickAngle <= 315)
            {
                stickAngle = 270;
            }

            return stickAngle;
        }
        else
        {
            Debug.Log("hi :D");
            return stickNull;
        }
    }
    
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
        rb.linearVelocity = new Vector3(x + kbVel.x, rb.linearVelocity.y, rb.linearVelocity.z);
        // Debug.Log("linear x velocity set to " + x);
    }

    public void EditYV(float y) {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, y, rb.linearVelocity.z);
        // Debug.Log("linear y velocity set to " + y);
    }
}

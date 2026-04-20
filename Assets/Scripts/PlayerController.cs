using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UIElements.Experimental;
using System;
// using System.Numerics;

public class PlayerController : MonoBehaviour
{
    public GameObject NAtk;
    public GameObject FAtk;
    public GameObject DAtk;
    public GameObject UAtk;

    public GameObject NAir;
    public GameObject FAir;
    public GameObject BAir;
    public GameObject DAir;
    public GameObject UAir;

    public GameObject FSAtk;
    public GameObject DSAtk;
    public GameObject USAtk;

    private string uSpec;
    private string fSpec;
    private string nSpec;
    private string dSpec;

    public int playerID;

    private Rigidbody rb;
    private float moveSpeed;                    // how fast the player goes on the ground, and also the fastest a player can move in the air (ignoring knockback velocity)
    
    
    private float toJump;                       // important probably
    private float toFall;

    private float jumpDecay = 0.5f;
    private float fallDecay = 1.5f;

    private float jumpForce = 22;               // base jump multi (might become obsolete) (NOT OBSOLETE DONT DELETE)
    private float terminalVel = 40;            // this ones probably useless



    private float shortJump;

    private float jumpValue;                    // returns 1 if player is jumping, 0 if not
    private bool jumpFrame;
    private float prevJumpValue;
    private bool jumping;                       // returns true if the player is in the jumping state, false if it is not. technically redundant but booleans are so much easier to read
    private bool activeJumping;
    private bool prevActiveJumping;
    private bool shortJumping;
    private float jumpDelay;
    private float jumps = 0;                    // how many jumps the player has left
    

    private bool onGround;                      // raycast determines whether character is on ground or not

    private float fastFallSpeed = -20;          // constant speed of fast fall
    private float dodgeCooldown = 0;
    private float dodgeForce = 10;
    private float chargingStrong;
    private bool justStrongAttacked = false;

    private bool cantMove = false;
    private bool stun = false;

    private float stickNull = 6741;             // arbitrary value used for checking if stick is centered

    private Vector2 moveValue;                  // raw joystick input
    private float curvedMoveValue;              // curves the joystick x input, used by x movement calculations

    private float kbVel;                        // velocity from knockback, calculated separately from movement velocity
    private float kbVelDecay = 1;               // how much velocity from knockback decays every update

    private float airVel;                       // for changing velocity in mid-air; changes based on stick input
    private float airVelAdd = 1.5f;             // how much airVel changes every update

    private Collider playerCollider;            // collider component of the player
    private OnGroundChecker onGroundChecker;
    private bool prevGrounded;                  // used to check if player landed on the frame and reset double jumps
    public LayerMask ground;                    // defines the ground layer for the player

    private SpecialsLib specialsLib;            // references the specials library script attached to the player
    public Renderer rend;


    public float KB;                            // VARIABLES SENT TO UI
    public int lives;

    public bool inAttack = false;                       //Unimplemented yet but will be placed in the universal canmove checker when we do that
    public bool inAirAttack = false;
    public bool mirror;


    // UNITY STUFF ------------------------------------------------------------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameManager gm = FindFirstObjectByType<GameManager>();
        playerCollider = GetComponent<CapsuleCollider>();
        gm.AddPlayer(this);
        lives = 3;
        shortJump = 1;
        specialsLib = GetComponent<SpecialsLib>();
        onGroundChecker = GameObject.Find("groundHb").GetComponent<OnGroundChecker>();

        playerID = GetComponent<PlayerInput>().playerIndex;
        Debug.Log("Player index " + playerID + " spawned");

        transform.position = new Vector3(transform.position.x, transform.position.y + (-0.01f * playerID), transform.position.z);

        // rb.useGravity = false;
    }

    void FixedUpdate()
    {

        moveSpeed = 16;   // maximum movespeed

        // GROUND DETECTION
        prevGrounded = onGround;
        if (onGroundChecker.Check())
        {
            onGround = true;
            if (prevGrounded != onGround)
            {
                DisableAllAttacks();
                jumps = 2;
                toJump = jumpForce;
                activeJumping = false;
            }
            // Debug.Log("grounded");
        }
        else
        {
            onGround = false;
            // Debug.Log("ungrounded or broken");
        }


        // MOVEMENT UPDATES

        if (canMoveChecker() && onGround) // IF GROUNDED AND CAN MOVE
        // if (onGround)
        {
            airVel = 0;
            EditXV(curvedMoveValue * moveSpeed);
        }

        else if (!cantMove && !onGround) // IF UNGROUNDED AND CAN MOVE
        // else if (!onGround)
        {
            airVel = airVelAdd * curvedMoveValue;
            if (Mathf.Abs(rb.linearVelocity.x + airVel) > moveSpeed)
            {
                airVel = 0;
                EditXV(moveSpeed * Mathf.Sign(rb.linearVelocity.x));
            }
            else
            {
                EditXV(rb.linearVelocity.x + airVel);  // i dont know why this works the way it does. but it works lmao
            }
        }

        else
        {
            EditXV(0);
            Debug.Log("huh");  // if you see this message then cantMove is currently set to true
        }


        // KB VELOCITY DECAY

        if (Mathf.Abs(kbVel) > kbVelDecay)
        {
            kbVel -= kbVelDecay * Mathf.Sign(kbVel);
        }
        else
        {
            kbVel = 0;
        }


        // FASTFALL

        if (moveValue.y <= -0.5 && !onGround)
        {
            EditYV(fastFallSpeed);
        }

        // JUMP

        // if ((!onGround || jumping))
        // {
        //     // toJump = -Mathf.Sqrt(Mathf.Pow(4 * jumpDuration, 2) + 4) - (0.5f * jumpDuration) + 6;

        //     if (jumpDuration == 0.4f && jumping == false && jumps == 1)
        //     {
        //         shortJump = 0.6f;
        //     }
            
        //     toJump = FuckassJumpCalculator(jumpDuration, shortJump);

        //     EditYV(toJump);

        //     Debug.Log("input: " + jumpDuration + " - output: " + toJump);

        //     jumpDuration += 0.1f;
        // }
        // else if (onGround)
        // {
        //     jumpDuration = 1.348f; // maximum of the jump pos equation
        // }
        // else
        // {
        //     jumpDuration = 0;
        // }

        if (jumping && prevJumpValue == 0)
        {
            jumpFrame = true;
        }
        else
        {
            jumpFrame = false;
        }

        prevJumpValue = jumpValue;

        if (onGround)
        {
            toJump = 0;
            activeJumping = false;
        }

        prevActiveJumping = activeJumping;

        if (jumping && !activeJumping)
        {
            jumpDelay++;
        }

        if (jumpFrame && !onGround && jumps > 0)
        {
            jumpDelay = 0;
            toJump = 0;
            toFall = 0;
            activeJumping = true;
            shortJumping = false;
            Debug.Log("air jump");
        }

        if (!jumping && jumpDelay > 0 && jumpDelay < 5)
        {
            jumpDelay = 0;
            activeJumping = true;
            shortJumping = true;
            Debug.Log("short jump");
        }

        if (jumping && jumpDelay > 4)
        {
            jumpDelay = 0;
            activeJumping = true;
            shortJumping = false;
            Debug.Log("long jump");
        }
        
        
        if (activeJumping)
        {
            // Debug.Log("ts got called");
            if (prevActiveJumping != activeJumping || (jumpFrame && !onGround && jumps > 0))
            {
                if (onGround) { onGroundChecker.FuckYouYoureWrong(); }
                jumps--;
                if (shortJumping)
                {
                    toJump = jumpForce / 1.5f;
                    toFall = fallDecay * 5;
                }
                else
                {
                    toJump = jumpForce;
                }
            }
            else if (toJump > 0)
            {
                toJump -= jumpDecay;
            }

            // toJump += rb.linearVelocity.y;
        }

        if (!onGround)
        {
            toFall += fallDecay;
            Debug.Log("toFall: " + toFall);
            if (toFall > terminalVel)
            {
                toFall = terminalVel;
                Debug.Log("terminal velocity!");
            }
        }

        if (onGround)
        {
            toFall = 0;
        }

        EditYV(toJump - toFall);
        // EditYV(toJump);

        // Debug.Log($"jumping: {jumping} | jumpFrame: {jumpFrame} | jumps: {jumps} | jumpValue: {jumpValue} jumpDelay: {jumpDelay} | activeJumping: {activeJumping} | toJump: {toJump} | toFall: {toFall} | onGround: {onGround}");


        // mirroring

        if ((moveValue.x > 0) && canMoveChecker() && onGround)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            mirror = true;
        }
        if ((moveValue.x < 0) && canMoveChecker() && onGround)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            mirror = false;
        }

    }


    // COROUTINES ------------------------------------------------------------------------------------------
    public IEnumerator IFrames() {
        Debug.Log("iframes start");
        yield return new WaitForSeconds(2.67f);
        Debug.Log("iframes end");
        cantMove = false;
    }

    public IEnumerator WaitToDelete(float s, GameObject gb) {
        yield return new WaitForSeconds(s);
        gb.SetActive(false);
    }

    public IEnumerator WaitToDisable(float s, bool b) {
        yield return new WaitForSeconds(s);
        b = !b;
    }

    public IEnumerator AttackLagHandler(float sLag, float aLength, float eLag, GameObject gb, bool inAir) // inputs are: start lag, active hitbox length, end lag, hitbox object, is this an air attack
    {
        if (inAir) { inAirAttack = true; }
        inAttack = true;
        yield return new WaitForSeconds(sLag);
        gb.SetActive(true);
        yield return new WaitForSeconds(aLength);
        gb.SetActive(false);
        yield return new WaitForSeconds(eLag);
        inAttack = false;
        if (inAir) { inAirAttack = false; }
    }

    public IEnumerator AttackLagHandler(GameObject[] hitboxes, bool inAir) 
    {
        if (inAir) { inAirAttack = true; }
        inAttack = true;
        for (int i = 0; i < hitboxes.Length; i ++) 
        {
            float[] atkData = hitboxes[i].GetComponent<attack>().GetLagPack();
            yield return new WaitForSeconds(atkData[0]);
            hitboxes[i].SetActive(true);
            yield return new WaitForSeconds(atkData[1]);
            hitboxes[i].SetActive(false);
            yield return new WaitForSeconds(atkData[2]);
            inAttack = false;
            if (inAir) { inAirAttack = false; }
        }
    }

    public IEnumerator AttackLagHandler(float sLag, float aLength, float eLag, GameObject gb) //for strong attacks
    {
        Debug.Log("strong attack step one active");
        inAttack = true;
        yield return new WaitForSeconds(sLag);
        for (int i = 0; i < 120 * Time.deltaTime; i++)
        {
            if (chargingStrong == 1) {
                Debug.Log("charging strong attack for " + i + " frames");
            }
            else
            {
                StartCoroutine(AttackLagHandler(sLag, aLength, eLag, gb, false));
                yield break;
            }
        }
        StartCoroutine(AttackLagHandler(sLag, aLength, eLag, gb, false));
        yield break;
    }

    //▮▮▮▮▮▮▮    ▮▮▮    ▮▮  ▮▮▮▮▮▮▮    ▮▮       ▮▮  ▮▮▮▮▮▮
    //▮▮▮▮▮▮▮    ▮▮▮    ▮▮  ▮▮▮▮▮▮▮    ▮▮       ▮▮  ▮▮▮▮▮▮
    //   ▮▮▮        ▮▮ ▮▮ ▮▮  ▮▮       ▮▮   ▮▮       ▮▮     ▮▮
    //   ▮▮▮        ▮▮ ▮▮ ▮▮  ▮▮       ▮▮   ▮▮       ▮▮     ▮▮
    //   ▮▮▮        ▮▮    ▮▮▮  ▮▮▮▮▮▮▮    ▮▮       ▮▮      ▮▮
    //▮▮▮▮▮▮▮    ▮▮    ▮▮▮  ▮▮             ▮▮▮▮▮▮▮▮     ▮▮
    //▮▮▮▮▮▮▮    ▮▮      ▮▮  ▮▮              ▮▮▮▮▮▮        ▮▮
    public void Jump(InputValue value)
    {
        // if (jumps > 0 && !cantMove) {

            jumpValue = value.Get<float>();
            

            if (jumpValue == 1)
            {
                
                jumping = true;
                shortJump = 1;
                
                // jumpDuration = 0;
                
                // Debug.Log("jumpValue: " + jumpValue);
            }
            else
            {
                jumping = false;
                jumpFrame = false;
                
            }


            

            //Debug.Log("jump button value is uh " + jumpValue + " i think");
        // }
        // else
        // {
        //     jumping = false;
            
        // }
    }

    public void Release()
    {
        EditYV(rb.linearVelocity.y * 2);
    }

    public void Attack(InputValue value) 
    {
        float angle = SimplifyStickAngle();
        if (canMoveChecker() && !onGroundChecker.Check())
        // if (true)
        {
            if (angle == stickNull)
            {
                SortAttackType(NAir);
            }
            else if (angle == 0 || angle == 180)
            {
                if (BackAirInput()) { SortAttackType(BAir); }
                else { SortAttackType(FAir); }
            }
            else if (angle == 90)
            {
                SortAttackType(UAir);
            }
            else if (angle == 270)
            {
                SortAttackType(DAir);
            }
        }
        else 
        {
            if (angle == stickNull)
            {
                SortAttackType(NAtk);
            }
            else if (angle == 0 || angle == 180)
            {
                SortAttackType(FAtk);
            }
            else if (angle == 90)
            {
                SortAttackType(UAtk);
            }
            else if (angle == 270)
            {
                SortAttackType(DAtk);
            }
        }
    }

    public void StrongAttack(InputValue value) 
    {
        chargingStrong = value.Get<float>();
        justStrongAttacked = true;
        float angle = SimplifyStickAngle();

        if (!cantMove && chargingStrong == 1 && !inAttack)
        {
            if ((angle == 0 || angle == 180 || angle == stickNull) && onGround)
            {
                SortAttackType(FSAtk);
            }
            else if (angle == 90 && onGround)
            {
                SortAttackType(USAtk);
            }
            else if (angle == 270 && onGround)
            {
                SortAttackType(DSAtk);
            }
        }

        Debug.Log("Strong Attack in direction " + angle);
    }
    
    public void Special(InputValue value)
    {
        float angle = SimplifyStickAngle();
        if (angle == stickNull)
        {
            // SpecialsLib.IndexSpecials("blast", value);
        }
        Debug.Log("Special in direction " + angle);
    }


    public void Move(InputValue value)
    {
        moveValue = value.Get<Vector2>();

        // gives raw stick input a curve for better feel
        if (Mathf.Abs(moveValue.x) < 0.2)
        {
            curvedMoveValue = 0;
        }
        else if (Mathf.Abs(moveValue.x) > 0.7)
        {
            curvedMoveValue = 1 * Mathf.Sign(moveValue.x);
        }
        else
        {
            curvedMoveValue = (2 * moveValue.x) - (Mathf.Sign(moveValue.x) * 0.4f);
        }
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
    public bool canMoveChecker() {
        if (inAttack && !inAirAttack)
        {
            return false;
        }
        else if (stun)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool BackAirInput()
    {
        if (mirror && SimplifyStickAngle() == 180) { Debug.Log("mirror plus 180deg"); return true; }
        else if (!mirror && SimplifyStickAngle() == 0) { Debug.Log("no mirror plus 0deg"); return true; }
        else { return false; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack")) 
        {
            //Debug.Log("ow");
            var atkStats = other.GetComponent<attack>();
            ReceiveAttack(atkStats.GetDamage(), atkStats.GetKb(), atkStats.GetAngle(), other.transform.position);
        }
        if (other.CompareTag("Blast Zone")) 
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    // MISC ------------------------------------------------------------------------------------------
    public void Die() {
        transform.position = new Vector3(0, 6, 0);
        rb.linearVelocity = new Vector3(0, 0, 0);
        kbVel = 0;
        lives --;
        KB = 0;
    }

    public void SortAttackType(GameObject attack) //processes an inputted attack and sends it into the correct attack lag handler
    {
        var isStrong = attack.GetComponent<attack>().IsStrong();
        string attackAttribute = attack.GetComponent<attack>().GetAttribute();
        if (attackAttribute == "reg")
        {
            Debug.Log("regular attack inputted!");
            float[] lagStats = attack.GetComponent<attack>().GetLagPack();
            if (isStrong) { StartCoroutine(AttackLagHandler(lagStats[0], lagStats[1], lagStats[2], attack)); }
            else { StartCoroutine(AttackLagHandler(lagStats[0], lagStats[1], lagStats[2], attack, !onGround)); }
        }
        else if (attackAttribute == "multi") 
        {
            GameObject[] attackPack = attack.GetComponent<attack>().GetChildren();
            StartCoroutine(AttackLagHandler(attackPack, !onGround));
        }
    }

    public void DisableAllAttacks() 
    {
        GameObject[] attacksList = {NAtk, FAtk, UAtk, DAtk, NAir, UAir, DAir, FAir, BAir, FSAtk, DSAtk, USAtk }; //list of every normal attack

        StopAllCoroutines(); //shuts off all coroutines

        for (int i = 0; i < attacksList.Length; i++) //begins a loop that repeats once for every item in the attacksList
        {
            if (attacksList[i].GetComponent<attack>().GetAttribute() == "multi") //if this is a multi-attack, parse and disable children instead of the parent
            {
                GameObject[] subAttackList = attacksList[i].GetComponent<attack>().GetChildren();
                for (int k = 0; k < subAttackList.Length; k++)
                {
                    subAttackList[k].SetActive(false);
                }
            }
            else //otherwise disable the found object
            {
                attacksList[i].SetActive(false);
            }
            inAttack = false;
            inAirAttack = false;
        }
    }

    // MATHY FUNCTIONS ------------------------------------------------------------------------------------------

    private void ReceiveAttack(float dmg, float kb, float angle, Vector3 origin)
    {
        cantMove = true;
        if (angle == 362)
        {
            if (KB > 60)
            {
                angle = 180 * Mathf.Deg2Rad;
            }
            else
            {
                angle = 60 * Mathf.Deg2Rad;
            }
        }
        else if (angle == 361)
        {
            angle = Mathf.Atan2((transform.position.x - origin.x) * Mathf.Deg2Rad, (transform.position.y - origin.y) * Mathf.Deg2Rad);
        }
        else 
        {
            angle *= Mathf.Deg2Rad;
        }
        KB += 0.1f * dmg;

        if (kb == 0)
        {
            stun = true;
            EditXV(0);
            EditYV(0);
            StartCoroutine(WaitToDisable(0.3f, stun));
        }
        else
        {
            EditYV(1 + ((Mathf.Pow(0.00000000007f * KB, 5.0f) + (0.03f * KB) + 1.0f) * kb) * Mathf.Sin(angle));
            kbVel = (1 + ((Mathf.Pow(0.00000000007f * KB, 5.0f) + (0.03f * KB) + 1.0f) * kb) * Mathf.Cos(angle));
        }

        Debug.Log("attack recieved: dmg = " + dmg + " kb = " + kb + " angle = " + angle);
        StartCoroutine(IFrames());
    }

    public float SimplifyStickAngle()
    {
        if (moveValue.y != 0)
        {
            float stickAngle = Mathf.Rad2Deg * Mathf.Atan(moveValue.y / moveValue.x);
            
            if (moveValue.x > 0)
            {
                stickAngle = (stickAngle + 405) % 360;
            }
            else
            {
                stickAngle = (stickAngle + 225) % 360;
            }

            stickAngle = ((int)(stickAngle / 90) * 90.0f);
            Debug.Log(stickAngle);

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

    public float FuckassJumpCalculator(float f, float multi)
    {
        f *= multi;
        return ((-((4 * (((4 * f) / multi) - 5.65f)) / Mathf.Sqrt(Mathf.Pow(((4 * f) / multi) - 5.65f, 2) + 4)) - (0.5f / Mathf.Pow(multi, 3))) + (multi * ((((5.6f * f) / Mathf.Pow(multi, 2)) - (7.56f / multi)) * Mathf.Pow(2.718f, -Mathf.Pow((((2 * f) / multi) - 2.7f), 2))))) * jumpForce * multi;
    }

    // VELOCITY FUNCTIONS ------------------------------------------------------------------------------------------

    public void AddLinearVelocity(float x, float y, float z) {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x + x, rb.linearVelocity.y + y, rb.linearVelocity.z + z);
    }

    public void MultLinearVelocity(float x, float y, float z) {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * x, rb.linearVelocity.y * y, rb.linearVelocity.z * z);
    }

    public void EditXV(float x) {
        rb.linearVelocity = new Vector3(x + kbVel, rb.linearVelocity.y, rb.linearVelocity.z);
        // Debug.Log("linear x velocity set to " + x);
    }

    public void EditYV(float y) {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, y, rb.linearVelocity.z);
        // Debug.Log("linear y velocity set to " + y);
    }
}

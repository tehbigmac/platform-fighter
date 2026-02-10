using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed;
    private float jumpForce = 6;

    private bool onGround;
    private float jumps = 0;

    private Vector2 moveValue;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onGround) {
            moveSpeed = 1;
        } else {
            moveSpeed = 1;
        }

        rb.AddForce(Vector3.left * moveSpeed * moveValue.x * -1, ForceMode.Force);
    }

    private void OnJump(InputValue value)
        {
            if (jumps > 0) {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                jumps --;
                onGround = false;
            }
        }

    private void OnMove(InputValue value)
        {
            moveValue = value.Get<Vector2>();
        }

    private void OnCollisionEnter(Collision collision) {
        onGround = true;
        jumps = 2;
    }
}

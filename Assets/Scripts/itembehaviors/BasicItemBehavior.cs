using UnityEngine;

public class BasicItemBehavior : MonoBehaviour
{
    private Rigidbody rb;
    private bool wasThrown = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.y > -20)
        {
            rb.linearVelocity = new Vector3 (rb.linearVelocity.x, rb.linearVelocity.y - 1, 0);
            //Debug.Log("lv = " + rb.linearVelocity.y);
        }
    }

    public void AddLinearVelocity(float x, float y, float z)
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x + x, rb.linearVelocity.y + y, rb.linearVelocity.z + z);
    }

    public void ActivateFunction()
    {
        wasThrown = true;
        Debug.Log("function on now hehe");
    }
    
    public bool GetThrown()
    {
        return wasThrown;
    }
}

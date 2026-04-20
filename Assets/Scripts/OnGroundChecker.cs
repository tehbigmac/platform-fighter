using UnityEngine;

public class OnGroundChecker : MonoBehaviour
{
    private Collider box;
    private bool onGround = false;
    private bool prevGrounded;

    void Start()
    {
        box = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Semisolid"))
        {
            onGround = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Semisolid"))
        {
            onGround = false;
        }
    }

    public void FuckYouYoureWrong()
    {
        onGround = false;
    }
    public bool Check() 
    {
        return onGround;
    }
}
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

    private void OnTriggerStay(Collider collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Semisolid"))
        {
            Debug.Log("trigger stay asd");
            transform.parent.GetComponent<PlayerController>().ToggleRaycast(true);
        
        }
        if (collision.gameObject.CompareTag("Semisolid")) 
        {
            transform.parent.GetComponent<PlayerController>().ToggleOnSemisolid(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        transform.parent.GetComponent<PlayerController>().ToggleOnSemisolid(false);
    }

    public void FuckYouYoureWrong()
    {
        Debug.Log("fuck you asd");
        onGround = false;
    }
    // public bool Check() 
    // {
    //     return onGround;
    // }
}
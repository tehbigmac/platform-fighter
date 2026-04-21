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
        if (!collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("trigger stay asd");
            transform.parent.GetComponent<PlayerController>().ToggleRaycast(true);
        
        }
    }

    // private void OnTriggerExit(Collider collision)
    // {
    //     if (!collision.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("trigger exit asd");
    //         onGround = false;
    //     }
    // }

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
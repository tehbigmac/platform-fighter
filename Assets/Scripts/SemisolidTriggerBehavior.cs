using UnityEngine;

public class SemisolidTriggerBehavior : MonoBehaviour
{
    private Collider collision;
    public GameObject platform;

    private void Start()
    {
        collision = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Motion trigger");
        other.GetComponent<PlayerController>().ToggleRaycast(false);
        platform.GetComponent<SemisolidPlatformBehavior>().IgnoreCollisionToggle(other, true);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Motion uhm un-trigger");

        if (other.gameObject.CompareTag("GHB")) { other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z); }
        else
        {
            other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
            other.GetComponent<PlayerController>().ToggleRaycast(true);
        }
        platform.GetComponent<SemisolidPlatformBehavior>().IgnoreCollisionToggle(other, false);
    }
}

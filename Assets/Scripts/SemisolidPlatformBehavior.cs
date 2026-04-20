using UnityEngine;

public class SemisolidPlatformBehavior : MonoBehaviour
{
    private Collider collision;

    private void Start()
    {
        collision = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Motion trigger");
        Physics.IgnoreCollision(other, collision, true);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Motion uhm un-trigger");
        other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 0.1f, other.transform.position.z);
        Physics.IgnoreCollision(other, collision, false);
    }
}

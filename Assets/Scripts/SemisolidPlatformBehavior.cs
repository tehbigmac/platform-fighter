using UnityEngine;

public class SemisolidPlatformBehavior : MonoBehaviour
{

    private Collider collision;

    private void Start()
    {
        collision = GetComponent<Collider>();
    }

    public void IgnoreCollisionToggle(Collider collider, bool b)
    {
        Physics.IgnoreCollision(collider, collision, b);
    }
}

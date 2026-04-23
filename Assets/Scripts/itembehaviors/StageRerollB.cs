using UnityEngine;

public class StageRerollB : MonoBehaviour
{
    private BasicItemBehavior bScript;
    private bool canRandomize;
    void Start()
    {
        bScript = GetComponent<BasicItemBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bScript.GetThrown())
        {
            canRandomize = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Pickup") && canRandomize)
        {
            StaticLib.stageRollReq = true;
            Object.Destroy(gameObject);
        }

    }
}

using UnityEngine;

public class DebugCommands : MonoBehaviour
{

    public GameObject sandBag;
    Vector3 spawn = new Vector3(0, 6, 0);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
           Instantiate(gameObject, spawn, Quaternion.Euler(0, 0, 0));
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class CameraManager : MonoBehaviour
{


    private GameObject[] players = new GameObject[2]; // PLAYER COUNT GOES HERE + KB UI MANGER

    private Vector2 lowerBounds;
    private Vector2 upperBounds;

    private Vector2 cameraPos;

    private float screenRatio;
    private float playerRatio;
    public Camera cam;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        screenRatio = Screen.width / Screen.height;
        Debug.Log(screenRatio);

        // rewrite ts later

        players[0] = GameObject.Find("meowl");
        players[1] = GameObject.Find("sandbag");
    }

    // Update is called once per frame
    void Update()
    {

        // Camera cam = Camera.main;
        lowerBounds = players[0].transform.position;
        upperBounds = players[0].transform.position;

        if(cam == null)
        {
            Debug.Log("idek");
        }
        

        transform.localScale = new Vector3 (100f, 100f, 100f);

        

        for (int i = 1; i < players.Length; i++) // start on second player (index 1)
        {
            if (players[i].transform.position.x < lowerBounds.x)
            {
                lowerBounds.x = players[i].transform.position.x;
            }
            if (players[i].transform.position.y < lowerBounds.y)
            {
                lowerBounds.y = players[i].transform.position.y;
            }
            if (players[i].transform.position.x > upperBounds.x)
            {
                upperBounds.x = players[i].transform.position.x;
            }
            if (players[i].transform.position.y > upperBounds.y)
            {
                upperBounds.y = players[i].transform.position.y;
            }
        }

        Vector2 cameraPos = (upperBounds + lowerBounds) / 2;
        transform.position = new Vector3(cameraPos.x, cameraPos.y, -20f);

        Vector2 posDifference = upperBounds - lowerBounds;

        cam.orthographicSize = posDifference.x * 0.7f;
        if (cam.orthographicSize < 5)
        {
            cam.orthographicSize = 5;
        }

        Debug.Log(posDifference.x * 0.7f);


        playerRatio = posDifference.x / posDifference.y;

        
        

        if (playerRatio > screenRatio)
        {
            
        }

    }
}

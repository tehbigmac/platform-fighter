using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class CameraManager : MonoBehaviour
{


    private GameObject[] players = new GameObject[0]; // PLAYER COUNT GOES HERE + KB UI MANGER

    private Vector2 lowerBounds;
    private Vector2 upperBounds;

    private float lowerInfX;
    private float lowerInfY;
    private float upperInfX;
    private float upperInfY;

    private Vector2 cameraPos;
    private Vector3 targetPos;

    private float screenRatio;
    private float playerRatio;

    private float targetSize;
    
    public Camera cam;
    private Vector3 refVelocity;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        screenRatio = Screen.width / Screen.height;
        Debug.Log(screenRatio);

        // rewrite ts later

        
        print(players);
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        // Camera cam = Camera.main;
        if (players.Length > 0)
        {
            lowerBounds = players[0].transform.position;
            upperBounds = players[0].transform.position;
        }

        if (cam == null)
        {
            Debug.Log("idek");
        }
        

        transform.localScale = new Vector3 (100f, 100f, 100f);

        
        if (players.Length > 0)
        {
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
        }

        // position code

        // Debug.Log("horizontal distance from center to leftmost: " + lowerBounds.x);
        // Debug.Log("adjusted distance from center to leftmost: " + (1 / (1 + Mathf.Pow(2.718f, 0.3f * (Mathf.Abs(lowerBounds.x) - 25)))));

        lowerBounds.x = CalculateInfluence(lowerBounds.x);
        lowerBounds.y = CalculateInfluence(lowerBounds.y);
        upperBounds.x = CalculateInfluence(upperBounds.x);
        upperBounds.y = CalculateInfluence(upperBounds.y);


        cameraPos = (upperBounds + lowerBounds) / 2;
        targetPos = new Vector3(cameraPos.x, cameraPos.y, -20f);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref refVelocity, 0.3f);

        // scaling code

        Vector2 posDifference = upperBounds - lowerBounds;

        playerRatio = posDifference.x / posDifference.y;

        if (playerRatio > screenRatio)
        {
            targetSize = (7 / (1 + Mathf.Pow(2.718f, -0.2f * (posDifference.x - 15)))) + 6;
        }
        else
        {
            targetSize = (7 / (1 + Mathf.Pow(2.718f, -0.2f * ((posDifference.y * screenRatio) - 15)))) + 6;
        }

        cam.orthographicSize += (5f * Time.deltaTime * (targetSize - cam.orthographicSize));

    }

    float CalculateInfluence(float bound)
    {
        bound *= (1 / (1 + Mathf.Pow(2.718f, 0.3f * (Mathf.Abs(bound) - 25))));
        return bound;
    }
}

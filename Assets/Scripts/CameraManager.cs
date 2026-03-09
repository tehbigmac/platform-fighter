using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public GameObject[] players = new GameObject[2]; // PLAYER COUNT GOES HERE + KB UI MANGER

    private Vector2 lowerBounds;
    private Vector2 upperBounds;

    private Vector2 cameraPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // rewrite ts later

        players[0] = GameObject.Find("meowl");
        players[1] = GameObject.Find("sandbag");
    }

    // Update is called once per frame
    void Update()
    {
        lowerBounds = players[0].transform.position;

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
    }
}

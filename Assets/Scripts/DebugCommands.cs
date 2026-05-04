using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DebugCommands : MonoBehaviour
{

    public GameObject sandBag;
    Vector3 spawn = new Vector3(0, 6, 0);
    public Transform canvas;


    private List<PlayerController> players;
    public TextMeshProUGUI debugPrefab;
    private TextMeshProUGUI[] debugUI = new TextMeshProUGUI[4];
    public Vector2[] debugUISpawnPositions = new Vector2[4];

    void Start()
    {
        players = FindFirstObjectByType<GameManager>().players;

        for (int i = 0; i < debugUISpawnPositions.Length; i++)
        {
            debugUI[i] = Instantiate(debugPrefab, canvas);
            
            RectTransform rect = debugUI[i].GetComponent<RectTransform>();
            rect.anchoredPosition = debugUISpawnPositions[i];

            debugUI[i].enabled = false;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
           Instantiate(gameObject, spawn, Quaternion.Euler(0, 0, 0));
        }
        if(Input.GetKeyDown(KeyCode.F3))
        {
            ToggleDebugMenu();
        }


        for (int i = 0; i < players.Count; i++)
        {
            debugUI[i].text = $@"

            KB: {players[i].KB}%
            toJump: {players[i].toJump}
            toFall: {players[i].toFall}
            canGetItem: {players[i].canGetItem}
            hasItem: {players[i].hasItem}
            canRaycast: {players[i].canRaycast}
            onSemisolidGround: {players[i].onSemisolidGround}
            onGround: {players[i].onGround}


            ";
            
        }
    }

    private void ToggleDebugMenu()
    {
        if (debugUI[0].enabled == false)
        {
            for (int i = 0; i < players.Count; i++)
            {
                debugUI[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < players.Count; i++)
            {
                debugUI[i].enabled = false;
            }
        }
        
    }
}
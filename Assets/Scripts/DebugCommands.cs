using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.Mathematics;
using System;

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

        debugUI[0].color = new Color(0.5f, 0.7f, 1.0f, 1.0f);
        debugUI[1].color = new Color(1.0f, 0.5f, 0.6f, 1.0f);
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

            inAttack: {players[i].inAttack}
            inAirAttack: {players[i].inAirAttack}

            cantMove: {players[i].cantMove}
            stun: {players[i].stun}
            stunTimer: {players[i].stunTimer}
            hardKB: {players[i].hardKB}

            stickAngle: {players[i].SimplifyStickAngle()}

            kbVel: {Math.Round(players[i].kbVel, 3)}
            kbYVel: {Math.Round(players[i].kbYVel, 3)}

            atkData.angle: {players[i].atkData.angle}
            {Mathf.Cos(players[i].atkData.angle)}
            {Mathf.Sin(players[i].atkData.angle)}

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
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class LivesUIManager : MonoBehaviour
{

    private List<PlayerController> players;

    public GameObject livesPrefab;
    public Transform canvas;
    

    private GameObject[] livesUI = new GameObject[4];
    private Image[] livesAssets;

    public Vector2[] livesUISpawnPositions = new Vector2[4];


    void Start()
    {
        players = FindFirstObjectByType<GameManager>().players;
        // livesAssets = FindFirstObjectByType<LivesUIAssigner>().livesAssets;

        for (int i = 0; i < livesUISpawnPositions.Length; i++)
        {
            livesUI[i] = Instantiate(livesPrefab, canvas);
            
            RectTransform rect = livesUI[i].GetComponent<RectTransform>();
            rect.anchoredPosition = livesUISpawnPositions[i];

            livesUI[i].SetActive(false);
        }
    }


    void Update()
    {
        for (int i = 0; i < players.Count; i++)
        {
            
            livesUI[i].SetActive(true);

            livesAssets = livesUI[i].GetComponent<LivesUIAssigner>().livesAssets;

            for (int j = 0; j < livesAssets.Length; j++)
            {
                livesAssets[j].enabled = j < players[i].lives;
                Debug.Log("comparing " + j + " with " +  players[i].lives);
            }
        }
    }
}

using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class KBUIManager : MonoBehaviour
{

    private List<PlayerController> players; // get players from game manager

    public TextMeshProUGUI KBPRefab;
    public Transform canvas;

    private TextMeshProUGUI[] playerKBDisplay = new TextMeshProUGUI[4];

    public Vector2[] KBUISpawnPositions = new Vector2[4];


    void Start()
    {

        players = FindFirstObjectByType<GameManager>().players;

        for (int i = 0; i < KBUISpawnPositions.Length; i++)
        {
            playerKBDisplay[i] = Instantiate(KBPRefab, canvas);
            RectTransform rect = playerKBDisplay[i].GetComponent<RectTransform>();
            rect.anchoredPosition = KBUISpawnPositions[i];

            playerKBDisplay[i].enabled = false;
        }
    }


    void Update()
    {
        
        for (int i = 0; i < players.Count; i++)
        {
            playerKBDisplay[i].text = players[i].KB.ToString("F1") + "%";
            playerKBDisplay[i].enabled = true;
        }
    }

    

    // void KBUIEffect(TextMeshProUGUI affectedGUI)
    // {
    //     affectedGUI.color = new Color(1f, 0f, 0f, 1f);
    // }
}



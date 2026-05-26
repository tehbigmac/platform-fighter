using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class KBUIManager : MonoBehaviour
{

    private List<PlayerController> players; // get players from game manager

    public TextMeshProUGUI KBPrefab;
    public Transform canvas;

    private TextMeshProUGUI[] KBUI = new TextMeshProUGUI[4];

    public Vector2[] KBUISpawnPositions = new Vector2[4];


    void Start()
    {
        players = FindFirstObjectByType<GameManager>().players;

        for (int i = 0; i < KBUISpawnPositions.Length; i++)
        {
            KBUI[i] = Instantiate(KBPrefab, canvas);
            RectTransform rect = KBUI[i].GetComponent<RectTransform>();
            rect.anchoredPosition = KBUISpawnPositions[i];

            KBUI[i].enabled = false;
        }

        KBUI[0].color = new Color(0.5f, 0.7f, 1.0f, 1.0f);
        KBUI[1].color = new Color(1.0f, 0.5f, 0.6f, 1.0f);
        KBUI[2].color = new Color(0.7f, 1.0f, 0.5f, 1.0f);
        KBUI[3].color = new Color(1.0f, 0.9f, 0.5f, 1.0f);

    }


    void Update()
    {
        
        for (int i = 0; i < players.Count; i++)
        {
            KBUI[i].text = players[i].KB.ToString("F1") + "%";
            KBUI[i].enabled = true;
        }
    }

    

    // void KBUIEffect(TextMeshProUGUI affectedGUI)
    // {
    //     affectedGUI.color = new Color(1f, 0f, 0f, 1f);
    // }
}



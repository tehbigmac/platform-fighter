using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class KBUIManager : MonoBehaviour
{

public float[] playerKB = new float[2]; // PLAYER COUNT GOES HERE AND ON DECLARATIONS BELOW + CAMERA MANGER
public float[] playerKBPrev = new float[2];
private TextMeshProUGUI[] playerKBDisplay = new TextMeshProUGUI[0];
public Vector2[] KBUISpawnPositions = new Vector2[4];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < playerKB.Length; i++)
        {
            playerKB[i] = 0.0f;
            playerKBDisplay[i].SetText(playerKB[i].ToString("F1") + "%");
        }
        for (int i = 0; i < playerKBPrev.Length; i++)
        {
            playerKBPrev[i] = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < playerKB.Length; i++)
        {
            playerKBPrev[i] = playerKB[i];
            //playerKBDisplay[i].color = new Color(1f, 1f, 1f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerKB[0] += 6.7f;
            Debug.Log("q shi");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerKB[1] += 4.1f;
            Debug.Log("e shi");
        }

        for (int i = 0; i < playerKB.Length; i++)
        {
            if (playerKBPrev[i] != playerKB[i])
            {
                playerKBDisplay[i].text = playerKB[i].ToString("F1") + "%";
            }
        }
    }

    // void KBUIEffect(TextMeshProUGUI affectedGUI)
    // {
    //     affectedGUI.color = new Color(1f, 0f, 0f, 1f);
    // }
}



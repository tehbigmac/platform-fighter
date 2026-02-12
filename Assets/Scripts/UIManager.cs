using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{


public float[] playerKB = new float[2]; // PLAYER COUNT GOES HERE AND ON DECLARATIONS BELOW
public float[] playerKBPrev = new float[2];
public TextMeshProUGUI[] playerKBDisplay = new TextMeshProUGUI[2]

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (int i in playerKB)
        {
            playerKB[i] = 0.0f;
        }
        foreach (int i in playerKBPrev)
        {
            playerKBPrev[i] = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (int i in playerKB)
        {
            playerKBDisplay[i].text = playerKB[i].ToString("F1") + "%";
            

            playerKBPrev[i] = playerKB[i];
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerOneKB += 6.7f;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerTwoKB += 4.1f;
        }

        if (playerOneKBPrev != playerOneKB)
        {
            
        }

        playerOneKBDisplay.text = playerOneKB.ToString("F1") + "%";
        playerTwoKBDisplay.text = playerTwoKB.ToString("F1") + "%";
    }

    void KBUIEffect(TextMeshProUGUI toEffect)
    {
        
    }
}


